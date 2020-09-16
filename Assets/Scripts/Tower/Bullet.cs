using Enemy;
using UnityEngine;
using Utils;

namespace Tower
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject impactEffect;
        private Transform _target;
        private Vector3 _targetPosition;
        private float _speed = 75f; // TODO
        private float _damage = 5; // TODO

        public void Seek(Transform target)
        {
            _target = target;
            _targetPosition = _target.position;
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
            DestroyBullet();

            if (!other.CompareTag(Tag.EnemyTag)) return;
            EnemyHit();
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

        private void EnemyHit()
        {
            var enemyState = _target.gameObject.GetComponent<EnemyState>();
            enemyState.AddDamage(_damage);
        }
    }
}
