using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy.Wave;
using UnityEngine;
using Utils.Messenger;

namespace Enemy.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] enemies;
        private readonly List<GameObject> _spawnedEnemies = new List<GameObject>();
        private bool _isCompleted;

        private Vector3 _spawnPosition;
        private IWave _wave;

        private string _filePath;

        public IWave Waves => _wave;

        public void Start()
        {
            _spawnPosition = transform.position;
            _spawnPosition.y = 0.5f;
        }

        public void Update()
        {
            if (_isCompleted) return;

            _wave?.Spawn(Time.deltaTime);

            if (_wave != null && (!_wave.IsCompleted() || IsAnyEnemyAlive())) return;

            Messenger.Broadcast(GameEvent.SPAWN_END);
            _isCompleted = true;
        }

        public void Spawn(Enemy.Type enemyType, int number, float elapseBetweenEnemy)
        {
            StartCoroutine(SpawnCoroutine(enemyType, number, elapseBetweenEnemy));
        }

        public void ReadWaves(string filePath)
        {
            _wave = SpawnParser.Parse(filePath, this);
            Debug.Log($"Number of waves: {_wave.NumberOfTotalWaves()}");
        }

        private IEnumerator SpawnCoroutine(Enemy.Type enemyType, int number, float elapseBetweenEnemy)
        {
            for (var i = 0; i < number; i++)
            {
                var enemy = Instantiate(
                    enemies[(uint) enemyType],
                    _spawnPosition,
                    Quaternion.identity
                );

                _spawnedEnemies.Add(enemy);

                yield return new WaitForSeconds(elapseBetweenEnemy);
            }
        }

        private bool IsAnyEnemyAlive()
        {
            return _spawnedEnemies
                .Where(e => e != null)
                .ToList()
                .Count != 0;
        }
    }
}
