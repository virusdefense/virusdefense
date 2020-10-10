using System;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

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
    }
}