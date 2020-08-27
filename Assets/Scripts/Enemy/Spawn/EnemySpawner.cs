using System;
using System.Collections;
using System.Collections.Generic;
using Enemy.Wave;
using UnityEngine;

namespace Enemy.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyA;
        [SerializeField] private GameObject enemyB;

        private Vector3 _spawnPosition;
        private IWave _wave;

        public void Start()
        {
            _spawnPosition = transform.position;
            _spawnPosition.y = 0.5f;

            _wave = MakeFakeWave();
        }

        public void Update()
        {
            _wave.Spawn(Time.deltaTime);
        }

        public void Spawn(EnemyType enemyType, int number, float elapseBetweenEnemy)
        {
            StartCoroutine(SpawnCoroutine(enemyType, number, elapseBetweenEnemy));
        }

        private IEnumerator SpawnCoroutine(EnemyType enemyType, int number, float elapseBetweenEnemy)
        {
            for (var i = 0; i < number; i++)
            {
                Instantiate(GetEnemy(enemyType)).transform.position = _spawnPosition;
                yield return new WaitForSeconds(elapseBetweenEnemy);
            }
        }

        private GameObject GetEnemy(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.A: return enemyA;
                case EnemyType.B: return enemyB;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
        }

        private IWave MakeFakeWave()
        {
            // TODO
            var waveInfo = new Dictionary<EnemyType, int>
            {
                {EnemyType.A, 5}, {EnemyType.B, 2}
            };

            var wave1 = new LeafWave(waveInfo, this, 2, 12);

            waveInfo = new Dictionary<EnemyType, int>
            {
                {EnemyType.A, 10}, {EnemyType.B, 3}
            };

            var wave2 = new LeafWave(waveInfo, this, 18, 30);

            return new Wave.Wave(new List<IWave> {wave1, wave2});
        }
    }
}