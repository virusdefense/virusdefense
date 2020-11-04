using System;
using Modifier;
using Tower;
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

        public static void SetUnlockedTowerLevel(TowerType.Type type, int level)
        {
            var key = Key.GROUND_TOWER_LEVEL;

            if (type == TowerType.Type.AIR_LIGHT)
                key = Key.LIGHT_TOWER_LEVEL;
            else if (type == TowerType.Type.AIR_HEAVY)
                key = Key.HEAVY_TOWER_LEVEL;

            PlayerPrefs.SetInt(key, level);
        }

        public static Optional<int> GetUnlockTowerLevel(TowerType.Type type)
        {
            if (type == TowerType.Type.GROUND)
                return GetUnlockLevelOfGroundTower();
            if (type == TowerType.Type.AIR_LIGHT)
                return GetUnlockLevelOfLightTower();

            return GetUnlockLevelOfHeavyTower();
        }

        private static Optional<int> GetUnlockLevelOfGroundTower()
        {
            return PlayerPrefs.HasKey(Key.GROUND_TOWER_LEVEL)
                ? new Optional<int>(PlayerPrefs.GetInt(Key.GROUND_TOWER_LEVEL))
                : new Optional<int>();
        }

        private static Optional<int> GetUnlockLevelOfLightTower()
        {
            return PlayerPrefs.HasKey(Key.LIGHT_TOWER_LEVEL)
                ? new Optional<int>(PlayerPrefs.GetInt(Key.LIGHT_TOWER_LEVEL))
                : new Optional<int>();
        }

        private static Optional<int> GetUnlockLevelOfHeavyTower()
        {
            return PlayerPrefs.HasKey(Key.HEAVY_TOWER_LEVEL)
                ? new Optional<int>(PlayerPrefs.GetInt(Key.HEAVY_TOWER_LEVEL))
                : new Optional<int>();
        }

        public static int GetTotalFounds()
        {
            var founds = 0;
            var level = 1;

            while (IsLevelCompleted(level).GetOrDefault(false))
            {
                founds += GetLevelScore(level).GetOrDefault(0);
                level++;
            }

            return founds;
        }

        public static Optional<int> GetTotalSpendsFound()
        {
            return PlayerPrefs.HasKey(Key.TOTAL_SPEND_FOUND)
                ? new Optional<int>(PlayerPrefs.GetInt(Key.TOTAL_SPEND_FOUND))
                : new Optional<int>();
        }

        public static int GetAvailableFounds()
        {
            return GetTotalFounds() - GetTotalSpendsFound().GetOrDefault(0);
        }

        public static void IncreaseSpendsFound(int spend)
        {
            PlayerPrefs.SetInt(
                Key.TOTAL_SPEND_FOUND,
                GetTotalSpendsFound().GetOrDefault(0) + spend
            );
        }

        public static Optional<int> GetModifierLevel(ModifierType.Type type)
        {
            var key = string.Format(Key.MODIFIER_LEVEL, type);

            return PlayerPrefs.HasKey(key)
                ? new Optional<int>(PlayerPrefs.GetInt(key))
                : new Optional<int>();
        }

        public static void SetModifierLevel(ModifierType.Type type, int level)
        {
            PlayerPrefs.SetInt(
                string.Format(Key.MODIFIER_LEVEL, type),
                level
            );
        }
    }
}
