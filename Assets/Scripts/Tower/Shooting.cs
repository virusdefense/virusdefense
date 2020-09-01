using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tower
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] public GameObject bullet;
        private GameObject _target;
        private const float Range = 10f;

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
            Debug.Log("SHOOT!");
            var bulletScript = Instantiate(bullet).GetComponent<Bullet>();
            bulletScript.Seek(_target.transform.position);
        }

        private void UpdateTarget()
        {
            var results = new Collider[100];
            var size = Physics.OverlapSphereNonAlloc(transform.position, Range, results);

            var nearEnemies = results.Where(obj => obj.CompareTag("Enemy"));

            var enemy = SelectEnemy(nearEnemies);

            _target = enemy.gameObject;
        }

        private static Collider SelectEnemy(IEnumerable<Collider> enemies)
        {
            // TODO
            return enemies.First();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Range);
        }
    }
}