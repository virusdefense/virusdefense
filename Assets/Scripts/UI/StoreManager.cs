using Manager;
using UnityEngine;

namespace UI
{
    public class StoreManager : MonoBehaviour
    {
        [SerializeField] private GameObject store;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void LateUpdate()
        {
            store.SetActive(_gameManager.IsStoreOpen);
        }
    }
}