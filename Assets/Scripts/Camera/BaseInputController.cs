using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputControllers
{ 
    public class BaseInputController
    {
        // directional buttons
        protected bool __Up;
        protected bool __Down;

        protected bool __Left;
        protected bool __Right;

        protected float __Scroll;



        protected float vert;
        protected float horz;

        protected float scroll;

        protected Vector3 TEMPVec3;
        protected Vector3 zeroVector = new Vector3(0, 0, 0);

        public bool Down
        {
            get { return __Down; }
        }

        public bool Up
        {
            get { return __Up; }
        }

        public bool Left
        {
            get { return __Left; }
        }

        public bool Right
        {
            get { return __Right; }
        }

        public float Scroll
        {
            get { return __Scroll; }
        }


        public virtual void CheckInput()
        {
            // override with your own code to deal with input
            horz = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
            scroll = Input.GetAxis("Mouse ScrollWheel");
        }
        public virtual float GetHorizontal()
        {
            return horz;
        }
        public virtual float GetVertical()
        {
            return vert;
        }

        public virtual float GetScroll()
        {
            return scroll;
        }

        public virtual Vector3 GetMovementDirectionVector()
        {

            if (Left || Right)
            {
                TEMPVec3.x = horz;
            }

            if (Up || Down)
            {
                TEMPVec3.z = vert;
            }

            if (Scroll !=0)
            {
                TEMPVec3.y = scroll;
            }
            return TEMPVec3;
        }

    }
}