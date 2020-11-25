using Controller;
using UnityEngine;
using UnityEngine.UI;
using Utils.Settings;

namespace UI
{
    public class UpgradeStoreManager : MonoBehaviour
    {
        [SerializeField] private Text availableFounds;
        [SerializeField] private Button homeButton;

        private void Awake()
        {
            homeButton.onClick.AddListener(UpgradeStoreController.LoadSelectLevel);
        }

        private void LateUpdate()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            availableFounds.text = SettingHelper.GetAvailableFounds()
                .ToString();
        }
    }
}
