using Tower;
using UI;
using UnityEngine;
using Utils;
using Utils.Messenger;

namespace Manager
{
    public class InputManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private TowerMenuManager _towerMenu;
        private SpawnTower _selectedBlock;
        private TowerState _selectedTowerState;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _towerMenu = FindObjectOfType<TowerMenuManager>();
        }

        private void Update()
        {
            if (_gameManager.IsTowerMenuOpen && Input.GetMouseButtonDown(0) && !Mouse.IsMouseOverUI())
            {
                Messenger.Broadcast(GameEvent.PLAY);
                return;
            }

            if (!Input.GetMouseButtonDown(0) || Mouse.IsMouseOverUI()) return;
            if (!Mouse.GetGameObjectPointed(out var hit)) return;

            if (hit.CompareTag(Tag.BuildBlockTag))
            {
                _selectedBlock = hit.GetComponent<SpawnTower>();
                ClickOnBuildBlock();
            }
            else if (hit.CompareTag(Tag.TowerTag))
            {
                _selectedTowerState = hit.GetComponent<TowerState>();
                _selectedBlock = _selectedTowerState.Block;
                OpenUpdateSellMenu();
            }
        }

        private void ClickOnBuildBlock()
        {
            if (_selectedBlock.IsFreeBlock)
                Messenger.Broadcast(GameEvent.STORE_OPEN);
            else
                OpenUpdateSellMenu();
        }

        private void OpenUpdateSellMenu()
        {
            _towerMenu.UpdateUI(_selectedBlock, _selectedTowerState);

            Messenger.Broadcast(GameEvent.TOWER_MENU_OPEN);
        }

        public void OnGroundTowerSelect()
        {
            Debug.Log("Click Ground Tower");
        }

        public void OnAirLightTowerSelect()
        {
            Debug.Log("Click Air Light Tower");
            BuildTower(TowerType.Type.AIR_LIGHT);
        }

        public void OnAirHeavyTowerSelect()
        {
            Debug.Log("Click Air Heavy Tower");
        }

        public void OnExit()
        {
            Messenger.Broadcast(GameEvent.PLAY);
        }

        public void OnSell()
        {
            _selectedBlock.SellTower();
            Messenger.Broadcast(GameEvent.PLAY);
        }

        public void OnUpgrade()
        {
            _selectedBlock.UpgradeTower();
            Messenger.Broadcast(GameEvent.PLAY);
        }

        private void BuildTower(TowerType.Type type)
        {
            _selectedBlock.Spawn(type);
            Messenger.Broadcast(GameEvent.PLAY);
        }
    }
}
