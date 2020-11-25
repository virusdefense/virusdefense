using System.Collections.Generic;
using Enemy.Spawn;


namespace Enemy.Wave
{
    public class LeafWave : IWave
    {
        private readonly float _waveDuration;
        private float _countDown;
        private bool _isStarted;
        private bool _isCompleted;

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
            return _isCompleted;
        }

        public bool IsStarted()
        {
            return _isStarted;
        }

        public int NumberOfTotalWaves()
        {
            return 1;
        }

        public int NumberOfPendingWaves()
        {
            return _isCompleted ? 0 : 1;
        }

        public int NumberOfSpawnedWaves()
        {
            return _isCompleted ? 1 : 0;
        }

        public int NumberOfStartedWaves()
        {
            return _isStarted ? 1 : 0;
        }

        public void Spawn(float deltaTime)
        {
            if (_isCompleted)
                return;

            _countDown -= deltaTime;

            if (_countDown >= 0)
                return;

            if (!_isStarted)
            {
                foreach (var enemyData in _enemies)
                    _enemySpawner.Spawn(enemyData.Key, enemyData.Value, _waveDuration / enemyData.Value);
                _isStarted = true;
            }

            if (_countDown + _waveDuration <= 0)
                _isCompleted = true;
        }

        public override string ToString()
        {
            return $"countDown: {_countDown}, duration: {_waveDuration}";
        }
    }
}
