using System;
using Enemy;
using Enemy.Spawn;
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

        private EnemySpawner _spawner;
        private PlayerState _playerState;

        private void Awake()
        {
            _playerState = FindObjectOfType<PlayerState>();
        }

        private void LateUpdate()
        {
            if (_spawner == null)
                _spawner = FindObjectOfType<EnemySpawner>();

            coinLabel.text = _playerState.Coin.ToString();
            lifeLabel.text = _playerState.Health.ToString();
            if (_playerState.Health < 0 )
            {
                lifeLabel.text = "0";
            }
            //TODO add update wave label
            Debug.Log(_spawner);
        }
    }
}