using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;
using Utils;

namespace Tower
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] public GameObject bullet;

        private Transform _target;
        public Transform partToRotate;
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
            
            if (_target == null) return;
            
            var dir = _target.transform.position - transform.position;
            var look = Quaternion.LookRotation(dir);
            var rotation = Quaternion.Lerp(partToRotate.rotation, look, Time.deltaTime * 8);
            partToRotate.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, rotation.eulerAngles.z);

            if (_countDown > 0) return;

            Shoot();
            _countDown = 1f / _state.FireRate;
        }

        private void Shoot()
        {
            var bulletGO = Instantiate(
                bullet,
                PositionHelper.OnTop(transform, 2.5f),
                Quaternion.identity
            );

            if (_state.Type == TowerType.Type.AIR_LIGHT)
                LightShootStrategy(bulletGO, _target);
            else
                HeavyShootStrategy(bulletGO, _target);
        }

        private void UpdateTarget()
        {
            ClearNearObjectsArray();
            Physics.OverlapSphereNonAlloc(transform.position, _state.Range, _nearObjects);

            var nearEnemies = _nearObjects
                .Where(obj => obj != null && obj.CompareTag(Tag.EnemyTag));

            var enemy = SelectEnemy(nearEnemies);
            _target = enemy == null ? null : enemy.transform;
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

        private void LightShootStrategy(GameObject bulletGO, Transform target)
        {
            bulletGO.GetComponent<Bullet>()
                .Shoot(target, _state.BulletSpeed, _state.BulletDamage);
        }

        private void HeavyShootStrategy(GameObject bulletGO, Transform target)
        {
            bulletGO.GetComponent<Bullet>()
                .Shoot(target, _state.BulletSpeed, _state.BulletDamage, _state.BulletRange);
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