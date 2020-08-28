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

        public string FilePath;

        public void Start()
        {
            _spawnPosition = transform.position;
            _spawnPosition.y = 0.5f;
        }

        public void Update()
        {
            _wave?.Spawn(Time.deltaTime);
        }

        public void Spawn(EnemyType enemyType, int number, float elapseBetweenEnemy)
        {
            StartCoroutine(SpawnCoroutine(enemyType, number, elapseBetweenEnemy));
        }

        public void ReadWaves(string filePath)
        {
            _wave = SpawnParser.Parse(filePath, this);
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
    }
}