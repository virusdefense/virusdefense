using UnityEngine;
using Utils;

namespace Tower
{
    public class SpawnTower : MonoBehaviour
    {
        [SerializeField] private GameObject tower;
        private float _towerHeight;

        private void Awake()
        {
            _towerHeight = tower.transform.localScale.y;
        }

        public void Spawn()
        {
            Instantiate(
                tower,
                PositionHelper.OnTop(transform, _towerHeight / 2),
                Quaternion.identity
            );
        }
    }
}
