using Tower;
using UnityEngine;
using Utils;

namespace Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameObject store;
        [SerializeField] private GameObject sellUpdate;
        private GameObject _selectedObject;
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

            _selectedObject = hit;

            if (hit.CompareTag(Tag.BuildBlockTag))
                ClickOnBuildBlock();
            else if (hit.CompareTag(Tag.TowerTag))
                OpenUpdateSellMenu();
        }

        private void LateUpdate()
        {
            sellUpdate.SetActive(_isSellUpdateOpen);
            store.SetActive(_isStoreOpen);
        }

        private void ClickOnBuildBlock()
        {
            var spawnTower = _selectedObject.GetComponent<SpawnTower>();

            if (spawnTower.IsFreeBlock)
                _isStoreOpen = true;
            else
            {
                _selectedObject = spawnTower.Tower;
                OpenUpdateSellMenu();
            }
        }

        private void OpenUpdateSellMenu()
        {
            _isSellUpdateOpen = true;

            sellUpdate.transform.position = PositionHelper.OnTop(
                _selectedObject.transform,
                _selectedObject.transform.localScale.y
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
            Debug.Log($"Sell {_selectedObject}");
            _isSellUpdateOpen = false;
        }

        public void OnUpdate()
        {
            Debug.Log($"Update {_selectedObject}");
            _isSellUpdateOpen = false;
        }

        private void BuildTower(TowerType.Type type)
        {
            var spawnTower = _selectedObject.GetComponent<SpawnTower>();
            spawnTower.Spawn(type);

            _isStoreOpen = false;
        }
    }
}
