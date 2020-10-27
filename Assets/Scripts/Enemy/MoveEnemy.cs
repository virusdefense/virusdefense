using System.Collections.Generic;
using System.Linq;
using Board;
using UnityEngine;
using Utils.Messenger;

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
                RetrievePath();
            else
            {
                if (!_state.IsMoving) return;

                if (Vector3.Distance(transform.position, _path[_pointIndex]) < 0.001f)
                {
                    if (_pointIndex + 1 < _path.Count)
                        _pointIndex++;
                    else
                        OnReachTarget();
                }

                var step = _state.Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _path[_pointIndex], step);
            }
        }

        private void RetrievePath()
        {
            var position = transform.position;
            var yPosition = position.y;

            _path = PathManager
                .GetInstance().SelectNearestPath(position)
                .Select(v => new Vector3(v.x, yPosition, v.z))
                .ToList();

            _state.targetPosition = _path.Last();
        }

        private void OnReachTarget()
        {
            Messenger<int>.Broadcast(GameEvent.ENEMY_REACH_TARGET, _state.HealthDamage);
            Destroy(gameObject);
        }
    }
}
