using System;
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

        private PlayerState _playerState;

        private void Awake()
        {
            _playerState = FindObjectOfType<PlayerState>();
        }

        private void LateUpdate()
        {
            coinLabel.text = _playerState.Coin.ToString();
            lifeLabel.text = _playerState.Health.ToString();
            if (_playerState.Health < 0 )
            {
                lifeLabel.text = "0";
            }
            //TODO add update wave label
        }
    }
}