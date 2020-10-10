using System;
using UnityEngine;

namespace Utils.Settings
{
    public static class SettingHelper
    {
        public static void SetLevelAsUnlocked(int level)
        {
            PlayerPrefs.SetInt(
                string.Format(Key.GAME_LEVEL_UNLOCKED, level),
                1
            );
        }

        public static Optional<bool> IsLevelUnlocked(int level)
        {
            var key = string.Format(Key.GAME_LEVEL_UNLOCKED, level);

            return PlayerPrefs.HasKey(key)
                ? new Optional<bool>(PlayerPrefs.GetInt(key) == 1)
                : new Optional<bool>();
        }

        public static void SetLevelAsCompleted(int level)
        {
            PlayerPrefs.SetInt(
                string.Format(Key.GAME_LEVEL_COMPLETED, level),
                1
            );
        }

        public static Optional<bool> IsLevelCompleted(int level)
        {
            var key = string.Format(Key.GAME_LEVEL_COMPLETED, level);

            return PlayerPrefs.HasKey(key)
                ? new Optional<bool>(PlayerPrefs.GetInt(key) == 1)
                : new Optional<bool>();
        }

        public static void SetLevelScore(int level, int score)
        {
            if (score < 0 || score > 3)
                throw new ArgumentOutOfRangeException($"score set to {score}, acepted only 0, 1, 2, 3");

            PlayerPrefs.SetInt(
                string.Format(Key.GAME_LEVEL_STAR, level),
                score
            );
        }

        public static Optional<int> GetLevelScore(int level)
        {
            var key = string.Format(Key.GAME_LEVEL_STAR, level);

            return PlayerPrefs.HasKey(key)
                ? new Optional<int>(PlayerPrefs.GetInt(key))
                : new Optional<int>();
        }

        public static Optional<int> GetUnlockLevelOfGroundTower()
        {
            return PlayerPrefs.HasKey(Key.GROUND_TOWER_LEVEL)
                ? new Optional<int>(PlayerPrefs.GetInt(Key.GROUND_TOWER_LEVEL))
                : new Optional<int>();
        }

        public static Optional<int> GetUnlockLevelOfLightTower()
        {
            return PlayerPrefs.HasKey(Key.LIGHT_TOWER_LEVEL)
                ? new Optional<int>(PlayerPrefs.GetInt(Key.LIGHT_TOWER_LEVEL))
                : new Optional<int>();
        }

        public static Optional<int> GetUnlockLevelOfHeavyTower()
        {
            return PlayerPrefs.HasKey(Key.HEAVY_TOWER_LEVEL)
                ? new Optional<int>(PlayerPrefs.GetInt(Key.HEAVY_TOWER_LEVEL))
                : new Optional<int>();
        }
    }
}
