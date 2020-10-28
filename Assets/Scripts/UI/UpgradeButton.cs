using Player;
using Tower;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Messenger;
using Utils.Settings;

namespace UI
{
    public class UpgradeButton : MonoBehaviour
    {
        private PlayerState _playerState;
        private Text _buttonText;
        private Button _button;
        private int _towerPrice;
        private const string OkText = "<b>Up</b>\n{0}";
        private const string NoMoneyText = "<b>Up</b>\n{0}\n<i>insufficient founds</i>";
        private bool _needToUpdate;
        private TowerType.Type _towerType;
        private int _towerLevel;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _buttonText = GetComponentInChildren<Text>();
            _playerState = FindObjectOfType<PlayerState>();

            Messenger.AddListener(GameEvent.COIN_CHANGE, OnCoinChange);
        }

        private void Update()
        {
            if (!_needToUpdate) return;

            var level = SettingHelper.GetUnlockTowerLevel(_towerType)
                .GetOrDefault(1);

            if (_towerLevel == level)
            {
                _button.interactable = false;
                return;
            }

            if (_playerState.Coin < _towerPrice)
            {
                _buttonText.text = string.Format(NoMoneyText, _towerPrice);
                _button.interactable = false;
            }
            else
            {
                _buttonText.text = string.Format(OkText, _towerPrice);
                _button.interactable = true;
            }

            _needToUpdate = false;
        }

        public void UpdateButton(TowerType.Type towerType, int towerLevel)
        {
            _towerLevel = towerLevel;
            _towerType = towerType;

            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(TowerFeatureFile, towerType, towerLevel + 1),
                SetFeature
            );

            _needToUpdate = true;
        }

        private void OnCoinChange()
        {
            _needToUpdate = true;
        }

        private void SetFeature(string featureName, string featureValue)
        {
            switch (featureName)
            {
                case "price":
                    _towerPrice = int.Parse(featureValue);
                    break;
            }
        }

        private const string TowerFeatureFile = "Plain/tower/tower_{0}_{1}";
    }
}
