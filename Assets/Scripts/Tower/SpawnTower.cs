using UnityEngine;
using Utils;

namespace Tower
{
    public class SpawnTower : MonoBehaviour
    {
        [SerializeField] private GameObject[] towers;
        private float _towerHeight;
        private GameObject _instantiateTower;

        public bool IsFreeBlock => _instantiateTower == null;
        public GameObject Tower => _instantiateTower;

        public void Spawn(TowerType.Type type)
        {
            _towerHeight = towers[(uint) type].transform.localScale.y;
            _instantiateTower = Instantiate(
                towers[(uint) type],
                PositionHelper.OnTop(transform, _towerHeight / 2),
                Quaternion.identity
            );
        }
    }
}
