using UnityEngine;
using UnityEngine.EventSystems;

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

        public static bool GetGameObjectPointed(out GameObject pointedObject)
        {
            var positionOnScreen = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(positionOnScreen);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity) && hit.collider != null)
            {
                pointedObject = hit.collider.gameObject;
                return true;
            }

            pointedObject = null;
            return false;
        }

        public static bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
