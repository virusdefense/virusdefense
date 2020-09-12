using System.Collections.Generic;
using System.Linq;
using Board;
using UnityEngine;

namespace Enemy
{
    public class MoveEnemy : MonoBehaviour
    {
        private EnemyState _state;
        private List<Vector3> _path;
        private int _pointIndex = 1;

        public void Awake()
        {
            _state = GetComponent<EnemyState>();
        }

        public void Update()
        {
            if (_path == null)
            {
                _path = PathManager.GetInstance().SelectNearestPath(transform.position);
                _state.targetPosition = _path.Last();
            }
            else
            {
                if (!_state.IsMoving) return;
                
                if (Vector3.Distance(transform.position, _path[_pointIndex]) < 0.001f)
                {
                    if (_pointIndex + 1 < _path.Count)
                        _pointIndex++;
                }

                var step = _state.Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _path[_pointIndex], step);
            }
        }
    }
}