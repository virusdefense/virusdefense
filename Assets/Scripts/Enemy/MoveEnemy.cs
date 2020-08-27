using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Enemy
{
    public class MoveEnemy : MonoBehaviour
    {
        private List<Vector3> _path;
        private int _pointIndex = 1;
        [SerializeField] private float speed;

        public void Update()
        {
            if (_path == null)
                _path = PathManager.GetInstance().SelectNearestPath(transform.position);
            else
            {
                if (Vector3.Distance(transform.position, _path[_pointIndex]) < 0.001f)
                {
                    if (_pointIndex + 1 < _path.Count)
                        _pointIndex++;
                }

                var step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _path[_pointIndex], step);
            }
        }
    }
}