using Manager;
using UnityEngine;

namespace UI.HUD.Modifiers
{
    public class ModifiersUiController : MonoBehaviour
    {
        [SerializeField] private GameObject modifiersUi;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            modifiersUi.SetActive(_gameManager.IsGameOnPlay || _gameManager.IsTowerMenuOpen);
        }
    }
}
