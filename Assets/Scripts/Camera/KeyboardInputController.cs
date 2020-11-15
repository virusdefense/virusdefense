using UnityEngine;

namespace InputControllers
{
    public class KeyboardInput : BaseInputController
    {
        public override void CheckInput()
        {
            horz = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
            zoomIn = Input.GetAxis("ZoomIn");
            zoomOut = Input.GetKeyUp(KeyCode.Space);
            scroll = Input.GetAxis("Mouse ScrollWheel");
            
            __Up = (vert > 0);
            __Down = (vert < 0);
            __Left = (horz < 0);
            __Right = (horz > 0);
            __ZoomIn = (zoomIn > 0);
            __ZoomOut = (zoomOut);
            __Scroll = (scroll);
        }
    }
}