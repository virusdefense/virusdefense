using System.Linq;
using Enemy;
using UnityEngine;
using Utils;

namespace Tower
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject impactEffect;
        private Vector3 _targetPosition;
        private float _speed;
        private float _damage;
        private float _range;

        // this is necessary to solve multiple triggering
        private bool _isTriggered;

        public void Shoot(Transform target, float speed, float damage, float range = 0)
        {
            _targetPosition = target.position;
            _speed = speed;
            _damage = damage;
            _range = range;
        }

        public void Update()
        {
            var step = _speed * Time.deltaTime;
            var position = transform.position;

            position = Vector3.MoveTowards(position, _targetPosition, step);
            transform.position = position;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_isTriggered) return;
            _isTriggered = true;

            DestroyBullet();
            Debug.Log($"bullet range: {_range}");

            // light mode
            if (_range == 0 && other.CompareTag(Tag.EnemyTag))
                EnemyHit(other.gameObject);
            // heavy mode
            else if (_range != 0)
                Detonation();
        }

        private void Detonation()
        {
            var nearObjects = new Collider[100];

            Physics.OverlapSphereNonAlloc(transform.position, _range, nearObjects);

            nearObjects
                .Where(obj => obj != null && obj.CompareTag(Tag.EnemyTag))
                .ToList().ForEach(c => EnemyHit(c.gameObject));
        }

        private void DestroyBullet()
        {
            var effect = Instantiate(
                impactEffect,
                transform.position + Vector3.up,
                transform.rotation
            );
            Destroy(gameObject);
            Destroy(effect, 1);
        }

        private void EnemyHit(GameObject enemy)
        {
            Debug.Log($"enemy: {enemy} -> {_damage}");
            var enemyState = enemy.GetComponent<EnemyState>();
            enemyState.AddDamage(_damage);
        }
    }
}
