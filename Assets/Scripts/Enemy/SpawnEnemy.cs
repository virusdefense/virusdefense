using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private void Start()
    {
        var position = transform.position;
        position.y = 0.5f;

        Instantiate(enemy).transform.position = position;
    }
}
