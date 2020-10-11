using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils.Settings;

namespace UI
{
    public class LevelButtonManager : MonoBehaviour
    {
        [SerializeField] private Image score1;
        [SerializeField] private Image score2;
        [SerializeField] private Image score3;
        [SerializeField] private Button button;
        [SerializeField] private int level;

        private void Awake()
        {
            GetComponentInChildren<Text>()
                .text = level.ToString();

            button.interactable = SettingHelper.IsLevelUnlocked(level)
                .GetOrDefault(level == 1);

            var score = SettingHelper.GetLevelScore(level).GetOrDefault(0);

            score1.enabled = score >= 1;
            score2.enabled = score >= 2;
            score3.enabled = score >= 3;

            button.onClick.AddListener(() =>
                SceneManager.LoadScene(string.Format(SceneName, level.ToString("00")))
            );
        }

        private const string SceneName = "Scenes/Levels/Level_{0}";
    }
}
