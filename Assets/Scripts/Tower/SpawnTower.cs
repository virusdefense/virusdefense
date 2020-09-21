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
        }

        public void SellTower()
        {
            _needToSellTower = true;
        }

        private void DestroyTower()
        {
            var moneyBack = Mathf.FloorToInt(_instantiateTowerState.Price * _playerState.ReturnRate);
            Messenger<int>.Broadcast(GameEvent.TOWER_SELLED, moneyBack);
            Destroy(_instantiateTower);
            _needToSellTower = false;
        }

        public void Spawn(TowerType.Type type)
        {
            _towerHeight = towers[(uint) type].transform.localScale.y;
            _instantiateTower = Instantiate(
                towers[(uint) type],
                PositionHelper.OnTop(transform, _towerHeight / 2),
                Quaternion.identity
            );
            _instantiateTowerState = _instantiateTower.GetComponent<TowerState>();
            _instantiateTowerState.Block = this;
        }
    }
}
