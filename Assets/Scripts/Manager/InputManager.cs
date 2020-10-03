using Tower;
using UI;
using UnityEngine;
using Utils;
using Utils.Messenger;

namespace Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameObject store;
        [SerializeField] private GameObject sellUpdate;
        [SerializeField] private SellButton sellButton;
        [SerializeField] private UpgradeButton upgradeButton;
        private SpawnTower _selectedBlock;
        private TowerState _selectedTowerState;
        private bool _isStoreOpen;
        private bool _isSellUpdateOpen;

        private void Awake()
        {
            store.SetActive(_isStoreOpen);
            sellUpdate.SetActive(_isSellUpdateOpen);
        }

        private void Update()
        {
            if (_isSellUpdateOpen && Input.GetMouseButtonDown(0) && !Mouse.IsMouseOverUI())
            {
                _isSellUpdateOpen = false;
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
            store.SetActive(_isStoreOpen);
        }

        private void ClickOnBuildBlock()
        {
            if (_selectedBlock.IsFreeBlock)
                _isStoreOpen = true;
            else
                OpenUpdateSellMenu();
        }

        private void OpenUpdateSellMenu()
        {
            sellButton.UpdateButton(_selectedTowerState.Price);
            upgradeButton.UpdateButton(_selectedTowerState.Type, _selectedTowerState.TowerLevel);

            _isSellUpdateOpen = true;
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
            _isStoreOpen = false;
        }

        public void OnSell()
        {
            _selectedBlock.SellTower();
            _isSellUpdateOpen = false;
        }

        public void OnUpgrade()
        {
            _selectedBlock.UpgradeTower();
            _isSellUpdateOpen = false;
        }

        public void OnPause()
        {
            Debug.Log("Pause button clicked");
            Messenger.Broadcast(GameEvent.PAUSE);
            
        }

        private void BuildTower(TowerType.Type type)
        {
            _selectedBlock.Spawn(type);
            _isStoreOpen = false;
        }
    }
}
