using System.Collections;
using Enemy.Wave;
using UnityEngine;

namespace Enemy.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemies;

        private Vector3 _spawnPosition;
        private IWave _wave;

        private string _filePath;

        public void Start()
        {
            _spawnPosition = transform.position;
            _spawnPosition.y = 0.5f;
        }

        public void Update()
        {
            _wave?.Spawn(Time.deltaTime);
        }

        public void Spawn(Enemy.Type enemyType, int number, float elapseBetweenEnemy)
        {
            StartCoroutine(SpawnCoroutine(enemyType, number, elapseBetweenEnemy));
        }

        public void ReadWaves(string filePath)
        {
            _wave = SpawnParser.Parse(filePath, this);
            Debug.Log(_wave.ToString());
        }

        private IEnumerator SpawnCoroutine(Enemy.Type enemyType, int number, float elapseBetweenEnemy)
        {
            for (var i = 0; i < number; i++)
            {
                Instantiate(enemies[(uint) enemyType]).transform.position = _spawnPosition;
                yield return new WaitForSeconds(elapseBetweenEnemy);
            }
        }

        
    }
}