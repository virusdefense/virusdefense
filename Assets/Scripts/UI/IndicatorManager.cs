using System;
using Enemy;
using Enemy.Spawn;
using Enemy.Wave;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class IndicatorManager : MonoBehaviour
    {
        [SerializeField] private Text coinLabel;
        [SerializeField] private Text lifeLabel;
        [SerializeField] private Text waveLabel;

        private IWave _waves;
        private PlayerState _playerState;

        private void Awake()
        {
            _playerState = FindObjectOfType<PlayerState>();
        }

        private void LateUpdate()
        {
            if (_waves == null)
                _waves = FindObjectOfType<EnemySpawner>().Waves;

            coinLabel.text = _playerState.Coin.ToString();
            lifeLabel.text = _playerState.Health.ToString();
            if (_playerState.Health < 0 )
            {
                lifeLabel.text = "0";
            }

            waveLabel.text = $"{_waves.NumberOfSpawnedWaves()} / {_waves.NumberOfTotalWaves()}";
        }
    }
}