using Controller;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectLevelUIManager : MonoBehaviour
    {
        [SerializeField] private Button storeButton;

        private void Awake()
        {
            storeButton.onClick.AddListener(
                SelectLevelController.LoadUpgradeStore
            );
        }
    }
}
