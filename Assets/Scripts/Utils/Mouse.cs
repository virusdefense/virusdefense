using UnityEngine;

namespace Utils
{
    public static class Mouse
    {
        public static bool GetMouseWorldPosition(out Vector3 mousePosition)
        {
            var positionOnScreen = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(positionOnScreen);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity) && hit.collider != null)
            {
                mousePosition = hit.point;
                return true;
            }


            mousePosition = Vector3.zero;
            return false;
        }
    }
}