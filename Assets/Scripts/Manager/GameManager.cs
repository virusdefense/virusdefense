using UnityEngine;
using Utils.Messenger;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private GameState _state;
        private int _numberOfSpawnPoint;
        private int _numberOfEndedSpawn;

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
            Messenger.AddListener(GameEvent.SPAWN_END, OnSpawnEnd);
            Messenger<int>.AddListener(GameEvent.BOARD_BUILD, OnBoardBuild);
        }

        private void LateUpdate()
        {
            Time.timeScale = _state == GameState.Play ? 1 : 0;

            if (_state == GameState.Won)
            {
                // TODO
                Debug.Log("Game won");
            }
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.PLAY, OnPlay);
            Messenger.RemoveListener(GameEvent.PAUSE, OnPause);
            Messenger.RemoveListener(GameEvent.WON, OnWon);
            Messenger.RemoveListener(GameEvent.OVER, OnGameOver);
            Messenger.RemoveListener(GameEvent.SPAWN_END, OnSpawnEnd);
            Messenger<int>.RemoveListener(GameEvent.BOARD_BUILD, OnBoardBuild);
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

        private void OnBoardBuild(int nSpawnPoint)
        {
            _numberOfSpawnPoint = nSpawnPoint;
        }

        private void OnSpawnEnd()
        {
            _numberOfEndedSpawn += 1;
            _state = _numberOfEndedSpawn == _numberOfSpawnPoint ? GameState.Won : _state;
        }

        private enum GameState
        {
            Play,
            Pause,
            Over,
            Won
        }
    }
}
