using UnityEngine;

namespace Tower
{
    public class SpawnTower : MonoBehaviour
    {
        [SerializeField] private GameObject tower;

        public void Spawn()
        {
            var spawnPosition = transform.position;
            spawnPosition.y = 0.5f;

            Instantiate(tower).transform.position = spawnPosition;
        }
    }
}