using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;

        private float _deltaBetweenWave = 5f;
        private float _countDown = 2f;

        private int _waveIndex;

        private Vector3 _spawnPosition;


        private void Start()
        {
            _spawnPosition = transform.position;
            _spawnPosition.y = 0.5f;
        }

        public void Update()
        {
            _countDown -= Time.deltaTime;

            if (_countDown > 0f)
                return;

            StartCoroutine(SpawnWave());
            _countDown = _deltaBetweenWave;
        }

        private IEnumerator SpawnWave()
        {
            _waveIndex++;
            for (var i = 0; i < _waveIndex; i++)
            {
                Instantiate(enemy).transform.position = _spawnPosition;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}