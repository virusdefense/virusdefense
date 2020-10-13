using Manager;
using UnityEngine;

namespace UI
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseButton;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void LateUpdate()
        {
            pauseButton.SetActive(_gameManager.IsGameOnPlay || _gameManager.IsTowerMenuOpen);
        }
    }
}
