using UnityEngine;

namespace InputControllers
{
    public class KeyboardInput : BaseInputController
    {

        public override void CheckInput()
        {

            horz = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
            scroll= Input.GetAxis("Mouse ScrollWheel");

            __Up = (vert > 0);
            __Down = (vert < 0);
            __Left = (horz < 0);
            __Right = (horz > 0);
            __Scroll = (scroll);
        }

    }
}