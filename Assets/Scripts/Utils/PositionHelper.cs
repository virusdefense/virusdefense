using UnityEngine;

namespace Utils
{
    public static class PositionHelper
    {
        public static Vector3 OnTop(Transform transform, float offset = 0)
        {
            var position = transform.position;
            var y = transform.localScale.y / 2;
            position.y += y + offset;
            return position;
        }
    }
}
