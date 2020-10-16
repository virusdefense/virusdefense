using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public int level;

    public Camera m_MainCamera;

    //Position camera
    private float positionX;
    private float positionY;
    private float positionZ;

    //Rotation camera
    private float rotationX = 65f;
    private float rotationY = -90f;
    private float rotationZ = 0f;

    private float offsetScroll = 0.25f;
    private float offsetPosition = 10f;

    //speed
    public float panSpeed = 30f;
    private float scrollSpeed = 5f;

    //scroll param
    private float minY;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        var board = ReadBoard(level);
        var rowNumber = board.Count;
        var columnNumber = board[0].Count;
        Debug.Log("row: " + rowNumber + ", " + "column: " + columnNumber );

        positionX = rowNumber*2;
        positionZ = columnNumber;
        positionY = columnNumber;

        minY = positionY - (positionY * offsetScroll);
        maxY = positionY + (positionY * offsetScroll);

        m_MainCamera = Camera.main;
        m_MainCamera.enabled = true;
        m_MainCamera.clearFlags = CameraClearFlags.SolidColor;
        m_MainCamera.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        m_MainCamera.transform.position = new Vector3(positionX, positionY, positionZ);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("" + m_MainCamera.transform.position.x);
        Debug.Log("" + transform.position.x);

        if (Input.GetKey("d") && m_MainCamera.transform.position.z <= positionZ + offsetPosition)
        {
            m_MainCamera.transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") && m_MainCamera.transform.position.z >= positionZ - offsetPosition)
        {
            m_MainCamera.transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("w") && m_MainCamera.transform.position.x >= positionX - offsetPosition)
        {
            m_MainCamera.transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") && m_MainCamera.transform.position.x <= positionX + offsetPosition/2)
        {
            m_MainCamera.transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = m_MainCamera.transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        m_MainCamera.transform.position = pos;

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
