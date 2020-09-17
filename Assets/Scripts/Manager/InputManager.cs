using Tower;
using UnityEngine;
using Utils;

namespace Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameObject store;
        private GameObject _selectedObject;
        private bool _listenClick = true;

        private void Awake()
        {
            store.SetActive(false);
        }

        private void Update()
        {
            if (!_listenClick) return;
                
            if (!Input.GetMouseButtonDown(0)) return;
            if (!Mouse.GetGameObjectPointed(out var hit)) return;

            _listenClick = false;
            
            _selectedObject = hit;
            
            if (hit.CompareTag(Tag.BuildBlockTag))
                ClickOnBuildBlock();
            else if (hit.CompareTag(Tag.TowerTag))
                OpenUpdateSellMenu();
        }

        private void ClickOnBuildBlock()
        {
            var spawnTower = _selectedObject.GetComponent<SpawnTower>();

            if (spawnTower.IsFreeBlock)
                OpenTowerStore();
            else
                OpenUpdateSellMenu();
        }

        private void OpenTowerStore()
        {
            store.SetActive(true);
        }

        private void OpenUpdateSellMenu()
        {
            Debug.Log("TODO: Sell and Update");
            _listenClick = true;
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

        private void BuildTower(TowerType.Type type)
        {
            var spawnTower = _selectedObject.GetComponent<SpawnTower>();
            spawnTower.Spawn(type);
            
            store.SetActive(false);
            _listenClick = true;
        }
    }
}
