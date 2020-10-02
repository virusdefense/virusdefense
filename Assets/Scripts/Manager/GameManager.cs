using UnityEngine;
using Utils.Messenger;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private GameState _state;

        public bool IsGameOver => _state == GameState.Over;
        public bool IsGameWon => _state == GameState.Won;
        public bool IsGameOnPlay => _state == GameState.Play;
        public bool IsGameOnPause => _state == GameState.Pause;

        private void Awake()
        {
            OnPlay();
            Messenger.AddListener(GameEvent.PLAY, OnPlay);
            Messenger.AddListener(GameEvent.PAUSE, OnPause);
            Messenger.AddListener(GameEvent.WON, OnWon);
            Messenger.AddListener(GameEvent.OVER, OnGameOver);
        }

        private void LateUpdate()
        {
            Time.timeScale = _state == GameState.Play ? 1 : 0;
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.PLAY, OnPlay);
            Messenger.RemoveListener(GameEvent.PAUSE, OnPause);
            Messenger.RemoveListener(GameEvent.WON, OnWon);
            Messenger.RemoveListener(GameEvent.OVER, OnGameOver);
        }

        private void OnPlay()
        {
            _state = GameState.Play;
            Debug.Log("On Play");
        }

        private void OnPause()
        {
            _state = GameState.Pause;
            Debug.Log("On Pause");
        }

        private void OnWon()
        {
            _state = GameState.Won;
            Debug.Log("On Won");
        }

        private void OnGameOver()
        {
            _state = GameState.Over;
            Debug.Log("On Game Over");
        }
        
        public enum GameState {Play, Pause, Over, Won}
    }
}