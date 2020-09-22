using UnityEngine;

namespace Utils.Settings
{
    public static class SettingHelper
    {
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
