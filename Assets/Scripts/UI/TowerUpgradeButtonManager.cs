using System;
using Controller;
using Tower;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Messenger;
using Utils.Settings;

namespace UI
{
    public class TowerUpgradeButtonManager : MonoBehaviour
    {
        [SerializeField] public TowerType.Type type;
        [SerializeField] public Feature fea1;
        [SerializeField] public Feature fea2;
        [SerializeField] public Feature fea3;
        [SerializeField] public Feature fea4;
        [SerializeField] public Feature fea5;
        [SerializeField] public Button buyButton;
        [SerializeField] public Text buyPrice;

        private UpgradeStoreController _controller;

        private float _defaultFireRate;
        private float _defaultRange;
        private float _bulletSpeed;
        private float _bulletDamage;
        private float _bulletRange;
        private string _towerName;
        private int _unlockPrice;
        private int _availableFounds;
        private int _level;

        private void Awake()
        {
            _controller = FindObjectOfType<UpgradeStoreController>();

            buyButton.onClick.AddListener(() =>
            {
                UpgradeStoreController.Unlock(type, _level, _unlockPrice);
            });
        }

        private void LateUpdate()
        {
           UpdateUI(); 
        }

        private void UpdateUI()
        {
            var unlockedLevel = SettingHelper.GetUnlockTowerLevel(type)
                .GetOrDefault(1);
            _level = unlockedLevel + 1;

            if (_level == 4)
            {
                buyButton.interactable = false;
                buyButton.GetComponentInChildren<Text>()
                    .text = "Already unlocked";

                buyPrice.text = "";

                fea1.Deactivate();
                fea2.Deactivate();
                fea3.Deactivate();
                fea4.Deactivate();
                fea5.Deactivate();

                return;
            }

            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(TowerFeatureFile, type, _level),
                SetFeature
            );

            _availableFounds = SettingHelper.GetAvailableFounds();

            buyPrice.text = _unlockPrice.ToString();
            buyButton.interactable = _availableFounds >= _unlockPrice;
            buyButton.GetComponentInChildren<Text>()
                .text = $"<b>{_towerName}</b>\n<i>Level {_level}</i>";

            fea1.SetName("Fire Rate");
            fea1.SetValue(_defaultFireRate.ToString());

            fea2.SetName("Range");
            fea2.SetValue(_defaultRange.ToString());

            fea3.SetName("Bul. Speed");
            fea3.SetValue(_bulletSpeed.ToString());

            fea4.SetName("Damage");
            fea4.SetValue(_bulletDamage.ToString());

            if (type == TowerType.Type.AIR_HEAVY)
            {
                fea5.SetName("Bullet Range");
                fea5.SetValue(_bulletRange.ToString());
            }
            else
                fea5.Deactivate();
        }

        private void SetFeature(string featureName, string featureValue)
        {
            switch (featureName)
            {
                case "fireRate":
                    _defaultFireRate = float.Parse(featureValue);
                    break;
                case "range":
                    _defaultRange = float.Parse(featureValue);
                    break;
                case "bulletSpeed":
                    _bulletSpeed = float.Parse(featureValue);
                    break;
                case "bulletDamage":
                    _bulletDamage = float.Parse(featureValue);
                    break;
                case "bulletRange":
                    _bulletRange = float.Parse(featureValue);
                    break;
                case "unlockPrice":
                    _unlockPrice = int.Parse(featureValue);
                    break;
                case "name":
                    _towerName = featureValue;
                    break;
            }
        }

        private const string TowerFeatureFile = "Plain/tower/tower_{0}_{1}";
    }
}
