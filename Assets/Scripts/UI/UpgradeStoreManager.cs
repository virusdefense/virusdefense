using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Messenger;
using Utils.Settings;

namespace UI
{
    public class UpgradeStoreManager : MonoBehaviour
    {
        [SerializeField] private Text availableFounds;
        [SerializeField] private Button homeButton;

        private void Awake()
        {
            Messenger.AddListener(GameEvent.UPGRADE_PURCHASED, OnPurchased);

            homeButton.onClick.AddListener(() =>
                SceneManager.LoadScene("Scenes/Select Level Scene")
            );

            UpdateUI();
        }

        private void UpdateUI()
        {
            availableFounds.text = SettingHelper.GetAvailableFounds()
                .ToString();
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.UPGRADE_PURCHASED, OnPurchased);
        }

        private void OnPurchased()
        {
            UpdateUI();
        }
    }
}
