using UnityEngine;
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
        [SerializeField] private Image score1;
        [SerializeField] private Image score2;
        [SerializeField] private Image score3;
        [SerializeField] private Button againButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button homeButton;

        private void Awake()
        {
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
        }

        public void OnGameWin(int score, int level)
        {
            text.text = "Game Win!";

            scoreBlock.SetActive(true);
            score1.enabled = score >= 1;
            score2.enabled = score >= 2;
            score3.enabled = score >= 3;

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
    }
}
