using Tower;
using UI;
using UnityEngine;
using Utils;
using Utils.Messenger;

namespace Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameObject sellUpdate;
        [SerializeField] private SellButton sellButton;
        [SerializeField] private UpgradeButton upgradeButton;
        private GameManager _gameManager;
        private SpawnTower _selectedBlock;
        private TowerState _selectedTowerState;
        private bool _isSellUpdateOpen;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            sellUpdate.SetActive(_isSellUpdateOpen);
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

        private void LateUpdate()
        {
            sellUpdate.SetActive(_isSellUpdateOpen);
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
            sellButton.UpdateButton(_selectedTowerState.Price);
            upgradeButton.UpdateButton(_selectedTowerState.Type, _selectedTowerState.TowerLevel);

            Messenger.Broadcast(GameEvent.TOWER_MENU_OPEN);
            
            sellUpdate.transform.position = PositionHelper.OnTop(
                _selectedBlock.Tower.transform,
                _selectedBlock.Tower.transform.localScale.y
            );
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
