using System;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;
using Utils.Messenger;

namespace UI
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void LateUpdate()
        {
            pauseMenu.SetActive(_gameManager.IsGameOnPause);
        }

        public void OnMainMenuClick()
        {
            Debug.Log("menu button clicked");
            Messenger.Broadcast(GameEvent.MAIN_MENU);
        }
        
        public void OnContinueClick()
        {
            Debug.Log("continue button clicked");
            Messenger.Broadcast(GameEvent.PLAY);
        }

        public void OnRestartClick()
        {
            Debug.Log("restart button clicked");
            Messenger.Broadcast(GameEvent.RELOAD_SCENE);
        }
    }
}