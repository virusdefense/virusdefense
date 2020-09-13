using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;

namespace Tower
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] public GameObject bullet;
        
        private GameObject _target;
        private readonly Collider[] _nearObjects = new Collider[NearObjectsArraySize];
        private float _countDown;
        private TowerState _state;

        public void Awake()
        {
            _state = GetComponent<TowerState>();
        }

        public void Start()
        {
            _countDown = 1f / _state.FireRate;

            InvokeRepeating(nameof(UpdateTarget), 0f, 0.25f);
        }

        public void LateUpdate()
        {
            _countDown -= Time.deltaTime;

            if (_countDown > 0) return;

            if (_target == null) return;

            Shoot();
            _countDown = 1f / _state.FireRate;
        }

        private void Shoot()
        {
            var bulletScript = Instantiate(bullet).GetComponent<Bullet>();
            bulletScript.Seek(_target.transform);
        }

        private void UpdateTarget()
        {
            ClearNearObjectsArray();
            var size = Physics.OverlapSphereNonAlloc(transform.position, _state.Range, _nearObjects);

            var nearEnemies = _nearObjects
                .Where(obj => obj != null && obj.CompareTag("Enemy"));

            _target = SelectEnemy(nearEnemies);
        }

        private static GameObject SelectEnemy(IEnumerable<Collider> enemies)
        {
            var minDistanceToTarget = float.MaxValue;
            GameObject closestToTarget = null;

            foreach (var enemyCollider in enemies)
            {
                var distToTarget = enemyCollider.gameObject.GetComponent<EnemyState>()
                    .DistanceToTarget;

                if (!(distToTarget < minDistanceToTarget)) continue;
                minDistanceToTarget = distToTarget;
                closestToTarget = enemyCollider.gameObject;
            }

            return closestToTarget;
        }

        /*
         * Remove old reference of enemies from result array.
         * If this step is skipped old enemies can remain on array of
         * nearest enemies even if they are out of range.
         */
        private void ClearNearObjectsArray()
        {
            for (var i = 0; i < NearObjectsArraySize; i++)
                _nearObjects[i] = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _state.Range);
        }

        private const int NearObjectsArraySize = 200;
    }
}