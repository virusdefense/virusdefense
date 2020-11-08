using System;
using Player;
using UI.Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Messenger;
using Utils.Settings;

namespace UI
{
    public class GameOverWinUIManager : MonoBehaviour
    {
        [SerializeField] private Canvas uiCanvas;
        [SerializeField] private GameObject scoreBlock;
        [SerializeField] private Text text;
        [SerializeField] private ScoreComponent lifeComponent;
        [SerializeField] private ScoreComponent coinComponent;
        [SerializeField] private Image score1;
        [SerializeField] private Image score2;
        [SerializeField] private Image score3;
        [SerializeField] private Button againButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button storeButton;

        private PlayerState _playerState;
        private float _currentFill;
        private float _fillTarget;

        private void Awake()
        {
            _playerState = FindObjectOfType<PlayerState>();

            uiCanvas.enabled = false;

            againButton.onClick.AddListener(() =>
                Messenger.Broadcast(GameEvent.RELOAD_SCENE)
            );

            homeButton.onClick.AddListener(() =>
                Messenger.Broadcast(GameEvent.MAIN_MENU)
            );

            nextButton.onClick.AddListener(() =>
                Messenger.Broadcast(GameEvent.NEXT_LEVEL)
            );

            storeButton.onClick.AddListener(() =>
                SceneManager.LoadScene("Scenes/Store scene")
            );

            score1.fillAmount = 0;
            score2.fillAmount = 0;
            score3.fillAmount = 0;
        }

        private void Update()
        {
            if (_currentFill >= _fillTarget) return;
            
            _currentFill += Time.deltaTime * FillSpeed;
            _currentFill = _currentFill > _fillTarget? _fillTarget : _currentFill;

            score1.fillAmount = _currentFill;
            score2.fillAmount = _currentFill;
            score3.fillAmount = _currentFill;
        }

        public void OnGameWin(int score, int level, int lifeWeight, int coinWeight)
        {
            text.text = "Game Win!";

            lifeComponent.MultiplierValue = lifeWeight;
            lifeComponent.TargetFill = _playerState.HealthRatio;

            coinComponent.MultiplierValue = coinWeight;
            coinComponent.TargetFill = _playerState.CoinRation;

            scoreBlock.SetActive(true);
            score1.enabled = score >= 1;
            score2.enabled = score >= 2;
            score3.enabled = score >= 3;

            _fillTarget = 1;

            nextButton.interactable = SettingHelper.IsLevelUnlocked(level + 1)
                .GetOrDefault(false);

            uiCanvas.enabled = true;
        }

        public void OnGameOver()
        {
            text.text = "Game Over";
            scoreBlock.SetActive(false);
            nextButton.gameObject.SetActive(false);
            uiCanvas.enabled = true;
        }
        
        private const float FillSpeed = 0.5f;
    }
}
