using System.Collections.Generic;
using Controller;
using Modifier;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Settings;

namespace UI.Store.Modifiers
{
    public class ModifierUpgradeButton : MonoBehaviour
    {
        [SerializeField] public ModifierType.Type type;
        [SerializeField] public Button buyButton;
        [SerializeField] public Text buyPrice;
        [SerializeField] public Text description;

        private int _unlockPrice;
        private int _availableFounds;
        private int _level;
        private string _modifierName;
        private string _modifierDescription;

        private void Awake()
        {
            Debug.Log("Modifier button");
            buyButton.onClick.AddListener(OnClick);
        }

        private void LateUpdate()
        {
            UpdateUI();
        }

        private void OnClick()
        {
            UpgradeStoreController.Unlock(type, _level, _unlockPrice);
        }

        private void UpdateUI()
        {
            var unlockedLevel = SettingHelper.GetModifierLevel(type)
                .GetOrDefault(0);
            _level = unlockedLevel + 1;
            Debug.Log($"level {_level}");

            if (_level == 3)
            {
                buyButton.interactable = false;
                buyButton.GetComponentInChildren<Text>()
                    .text = "Already unlocked";

                buyPrice.text = "";
                description.text = "";

                return;
            }

            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(TowerFeatureFile, type, _level),
                SetFeature
            );
            
            Debug.Log($"price: {_unlockPrice}");

            _availableFounds = SettingHelper.GetAvailableFounds();

            buyPrice.text = _unlockPrice.ToString();
            description.text = _modifierDescription;
            buyButton.interactable = _availableFounds >= _unlockPrice;
            buyButton.GetComponentInChildren<Text>()
                .text = $"<b>{_modifierName}</b>\n<i>Level {_level}</i>";
            
        }

        private void SetFeature(KeyValuePair<string, string> pair)
        {
            var featureName = pair.Key;
            var featureValue = pair.Value;
            
            Debug.Log($"{featureName}: {featureValue}");
            
            switch (featureName)
            {
                case "name":
                    _modifierName = featureValue;
                    break;
                case "description":
                    _modifierDescription = featureValue;
                    break;
                case "unlockPrice":
                    _unlockPrice = int.Parse(featureValue);
                    break;
            }
        }

        private const string TowerFeatureFile = "Plain/Modifier/modifier_{0}_{1}";
    }
}
