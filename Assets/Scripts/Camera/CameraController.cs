using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using InputControllers;

public class CameraController : MonoBehaviour
{
    [SerializeField] public int level;

    public Camera m_MainCamera;

    protected BaseInputController myInputController;

    //Position camera
    private float positionX;
    private float positionY;
    private float positionZ;

    //Rotation camera
    private float rotationX = 50f;
    private float rotationY = -90f;
    private float rotationZ = 0f;

    private int fieldOfView = 55;

    private float offsetScroll = 0.25f;
    private float offsetPosition = 10f;

    //speed
    private float panSpeed = 20f;
    private float scrollSpeed = 5f;

    //scroll param
    private float minY;
    private float maxY;

    ////////////////////////
    private Vector3 originalPosition;
    private Vector3 currentPosistion;
    private bool isZoomed;

    void Start()
    {
        var board = ReadBoard(level);
        var rowNumber = board.Count;
        var columnNumber = board[0].Count;
        Debug.Log("row: " + rowNumber + ", " + "column: " + columnNumber );

        positionX = (rowNumber*2) + offsetPosition;
        positionZ = columnNumber;
        positionY = columnNumber;

        minY = positionY - (positionY * offsetScroll);
        maxY = positionY + (positionY * offsetScroll);

        m_MainCamera = Camera.main;
        m_MainCamera.enabled = true;
        m_MainCamera.clearFlags = CameraClearFlags.SolidColor;
        m_MainCamera.fieldOfView = fieldOfView;
        m_MainCamera.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        m_MainCamera.transform.position = new Vector3(positionX, positionY, positionZ);

        myInputController = new KeyboardInput();
        isZoomed = false;

        originalPosition= new Vector3(positionX, positionY, positionZ); 
    }

    void Update()
    {
        MoveCamera();
        ZoomIn();
    }

    protected void MoveCamera()
    {
        myInputController.CheckInput();
        currentPosistion= m_MainCamera.transform.position;

        if (!isZoomed) { 
            if (myInputController.Right && currentPosistion.z <= positionZ + offsetPosition)
            {
                m_MainCamera.transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }
            if (myInputController.Left && currentPosistion.z >= positionZ - offsetPosition)
            {
                m_MainCamera.transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }
            if (myInputController.Up && currentPosistion.x >= positionX - offsetPosition)
            {
                m_MainCamera.transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
            if (myInputController.Down && currentPosistion.x <= positionX + offsetPosition / 2)
            {
                m_MainCamera.transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }
        else if (isZoomed)
        {
            if (myInputController.Right && currentPosistion.z <= positionZ + offsetPosition*2)
            {
                m_MainCamera.transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }
            if (myInputController.Left && currentPosistion.z >= offsetPosition/2)
            { 
                m_MainCamera.transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }
            if (myInputController.Up && currentPosistion.x >= 0)
            {
                m_MainCamera.transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
            if (myInputController.Down && currentPosistion.x <= positionX-offsetPosition)
            {
                m_MainCamera.transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }

        Vector3 pos = m_MainCamera.transform.position;
        pos.y -= myInputController.Scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        m_MainCamera.transform.position = pos;
    }

    protected void ZoomIn()
    {
        Vector3 mousePosition;
        var positionOnScreen = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(positionOnScreen);

        if (myInputController.ZoomIn && m_MainCamera.transform.rotation.eulerAngles.x != 90) 
        {
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity) && hit.collider != null)
            {
                mousePosition = hit.point;
                mousePosition.y = minY;
                m_MainCamera.transform.position = mousePosition;
                m_MainCamera.transform.rotation = Quaternion.Euler(90, rotationY, rotationZ);
                isZoomed = true;
            }
        }

        else if (myInputController.ZoomOut)
        {
            Debug.Log("Space released");
            m_MainCamera.transform.position = originalPosition;
            m_MainCamera.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
            isZoomed = false;
        }
    }

    private static List<List<char>> ReadBoard(int level)
    {
        return Resources.Load<TextAsset>(string.Format(LevelBoardFile, level.ToString("00")))
            .text.Split('\n')
            .Select(line => line.Split(' ')
                .Select(s => s[0]).ToList()
            ).ToList();
    }

    private const string LevelBoardFile = "Plain/Board/level_{0}";
}
