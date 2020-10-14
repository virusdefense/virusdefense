using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Messenger;
using Utils.Settings;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        private PlayerState _playerState;
        private GameManager _gameManager;
        private Boolean _isDone;
        private void Awake()
        {
            Messenger.AddListener(GameEvent.RELOAD_SCENE, OnReloadScene);
            Messenger.AddListener(GameEvent.MAIN_MENU,OnLoadMainMenu);
            _playerState = FindObjectOfType<PlayerState>();
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (_isDone) return;

            if (_gameManager.IsGameOver)
                OnGameOver();
            if(_gameManager.IsGameWon)
                OnGameWin();
        }

        private void OnGameWin()
        {
            var score = _playerState.GetScore();
            EndLevelSaves(score);

            _isDone = true;
        }
        
        private void OnGameOver()
        {
            
        }
        private void EndLevelSaves(int newScore)
        {
            Debug.Log("Save");
            
            var level = _playerState.level;
            
            SettingHelper.SetLevelAsCompleted(level);

            var oldScore = SettingHelper.GetLevelScore(level)
                .GetOrDefault(0);

            Debug.Log($"oldScore {oldScore}");
            Debug.Log($"newScore: {newScore}");

            if (newScore > oldScore)
                SettingHelper.SetLevelScore(level, newScore);

            for (var i = 1; i <= _playerState.UnlockNext; i++)
            {
                SettingHelper.SetLevelAsUnlocked(level + i);
            }
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.RELOAD_SCENE, OnReloadScene);
            Messenger.RemoveListener(GameEvent.MAIN_MENU, OnLoadMainMenu);
        }

        private void OnReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnLoadMainMenu()
        {
            //TODO
            Debug.Log("Load Main Menu");
        }
    }
}