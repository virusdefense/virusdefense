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
        private const float Range = 10f;
        private readonly Collider[] _nearObjects = new Collider[NearObjectsArraySize];

        private const float ShootingRate = 1f;
        private float _countDown;

        public void Start()
        {
            _countDown = 1f / ShootingRate;

            InvokeRepeating(nameof(UpdateTarget), 0f, 0.25f);
        }

        public void LateUpdate()
        {
            _countDown -= Time.deltaTime;

            if (_countDown > 0) return;

            if (_target == null) return;

            Shoot();
            _countDown = 1f / ShootingRate;
        }

        private void Shoot()
        {
            var bulletScript = Instantiate(bullet).GetComponent<Bullet>();
            bulletScript.Seek(_target.transform);
        }

        private void UpdateTarget()
        {
            ClearNearObjectsArray();
            var size = Physics.OverlapSphereNonAlloc(transform.position, Range, _nearObjects);

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
            Gizmos.DrawWireSphere(transform.position, Range);
        }

        private const int NearObjectsArraySize = 200;
    }
}