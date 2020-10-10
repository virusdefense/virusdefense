using System.Collections.Generic;
using Enemy.Spawn;


namespace Enemy.Wave
{
    public class LeafWave : IWave
    {
        private readonly float _waveDuration;
        private float _countDown;
        private bool _isSpawned;

        private readonly EnemySpawner _enemySpawner;
        private readonly Dictionary<Enemy.Type, int> _enemies;

        public LeafWave(Dictionary<Enemy.Type, int> enemies, EnemySpawner enemySpawner, float startTime, float endTime)
        {
            _enemies = enemies;
            _enemySpawner = enemySpawner;
            _waveDuration = endTime - startTime;
            _countDown = startTime;
        }

        public bool IsCompleted()
        {
            return _isSpawned;
        }

        public int NumberOfTotalWaves()
        {
            return 1;
        }

        public int NumberOfPendingWaves()
        {
            return _isSpawned ? 0 : 1;
        }

        public int NumberOfSpawnedWaves()
        {
            return _isSpawned ? 1 : 0;
        }

        public void Spawn(float deltaTime)
        {
            if (_isSpawned)
                return;

            _countDown -= deltaTime;

            if (_countDown >= 0)
                return;

            foreach (var enemyData in _enemies)
                _enemySpawner.Spawn(enemyData.Key, enemyData.Value, _waveDuration / enemyData.Value);

            _isSpawned = true;
        }

        public override string ToString()
        {
            return $"countDown: {_countDown}, duration: {_waveDuration}";
        }
    }
}
