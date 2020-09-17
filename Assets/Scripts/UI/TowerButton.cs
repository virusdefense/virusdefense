using System;
using Player;
using Tower;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Messenger;

namespace UI
{
    public class TowerButton : MonoBehaviour
    {
        [SerializeField] private TowerType.Type type;
        private string _towerName;
        private int _towerPrice;
        private Text _buttonText;
        private Button _button;
        private PlayerState _playerState;
        private string _okText;
        private string _noMoneyText;
        private bool _updateNeeded = true;


        private void Awake()
        {
            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(TowerFeatureFile, type),
                SetFeature
            );

            _button = GetComponent<Button>();
            _buttonText = GetComponentInChildren<Text>();
            _playerState = FindObjectOfType<PlayerState>();
            
            Debug.Log(_playerState.Coin);

            _okText = $"<b>{_towerName}</b>\n\n{_towerPrice}";
            _noMoneyText = $"<b>{_towerName}</b>\n\n{_towerPrice}\n\n<i>insufficient funds</i>";

            Messenger.AddListener(GameEvent.COIN_CHANGE, OnCoinChange);
        }

        private void Update()
        {
            if (!_updateNeeded) return;
            
            Debug.Log("Update ui");

            if (_towerPrice > _playerState.Coin)
            {
                _button.interactable = false;
                _buttonText.text = _noMoneyText;
            }
            else
            {
                _button.interactable = true;
                _buttonText.text = _okText;
            }

            _updateNeeded = false;
        }

        private void SetFeature(string featureName, string featureValue)
        {
            switch (featureName)
            {
                case "price":
                    _towerPrice = int.Parse(featureValue);
                    break;
                case "name":
                    _towerName = featureValue;
                    break;
            }
        }

        private void OnCoinChange()
        {
            _updateNeeded = true;
        }

        private const string TowerFeatureFile = "Plain/tower/tower_{0}";
    }
}