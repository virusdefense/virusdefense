using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Settings;

namespace UI
{
    public class LevelButtonManager : MonoBehaviour
    {
        [SerializeField] private GameObject score1;
        [SerializeField] private GameObject score2;
        [SerializeField] private GameObject score3;
        [SerializeField] private Button button;
        [SerializeField] private int level;

        private void Awake()
        {
            GetComponentInChildren<Text>()
                .text = level.ToString();

            button.interactable = SettingHelper.IsLevelUnlocked(level)
                .GetOrDefault(level == 1);

            var score = SettingHelper.GetLevelScore(level).GetOrDefault(0);

            score1.SetActive(score >= 1);
            score2.SetActive(score >= 2);
            score3.SetActive(score >= 3);
        }
    }
}
