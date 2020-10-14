using System;
using Player;
using UnityEngine;
using Utils;
using Utils.Messenger;

namespace Tower
{
    public class SpawnTower : MonoBehaviour
    {
        [SerializeField] private GameObject[] towers;
        private float _towerHeight;
        private GameObject _instantiateTower;
        private TowerState _instantiateTowerState;
        private PlayerState _playerState;
        private bool _needToSellTower;
        private bool _needToUpgrade;

        public bool IsFreeBlock => _instantiateTower == null;
        public GameObject Tower => _instantiateTower;

        private void Awake()
        {
            _playerState = FindObjectOfType<PlayerState>();
        }

        public void Update()
        {
            if (_needToSellTower)
                DestroyTower();
            if (!_needToUpgrade) return;
            var type = _instantiateTowerState.Type;
            var level = _instantiateTowerState.TowerLevel;

            Destroy(_instantiateTower);
            Spawn(type, level + 1);
            _needToUpgrade = false;
        }

        public void Spawn(TowerType.Type type)
        {
            Spawn(type, 1);
        }

        public void SellTower()
        {
            _needToSellTower = true;
        }

        public void UpgradeTower()
        {
            _needToUpgrade = true;
        }

        private void DestroyTower()
        {
            var moneyBack = Mathf.FloorToInt(_instantiateTowerState.Price * _playerState.ReturnRate);
            Messenger<int>.Broadcast(GameEvent.TOWER_SELLED, moneyBack);
            Destroy(_instantiateTower);
            _needToSellTower = false;
        }

        private void Spawn(TowerType.Type type, int towerLevel)
        {
            Debug.Log($"type: {type}, level: {towerLevel}");
            var index = (uint) type + (towerLevel - 1) * 3;
            //_towerHeight = towers[index].transform.localScale.y;
            _instantiateTower = Instantiate(
                towers[index],
                PositionHelper.OnTop(transform, 0.2f),
                Quaternion.identity
            );
            _instantiateTowerState = _instantiateTower.GetComponent<TowerState>();
            _instantiateTowerState.Block = this;
        }
    }
}
