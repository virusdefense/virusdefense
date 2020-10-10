using Manager;
using Tower;
using UnityEngine;
using Utils;

namespace UI
{
    public class TowerMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject towerMenu;
        [SerializeField] private SellButton sellButton;
        [SerializeField] private UpgradeButton upgradeButton;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void LateUpdate()
        {
            towerMenu.SetActive(_gameManager.IsTowerMenuOpen);
        }

        public void UpdateUI(SpawnTower selectedBlock, TowerState selectedTowerState)
        {
            sellButton.UpdateButton(selectedTowerState.Price);
            upgradeButton.UpdateButton(selectedTowerState.Type, selectedTowerState.TowerLevel);

            towerMenu.transform.position = PositionHelper.OnTop(
                selectedBlock.Tower.transform,
                selectedBlock.Tower.transform.localScale.y
            );
        }
    }
}