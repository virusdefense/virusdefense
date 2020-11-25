using Modifier;
using Tower;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Settings;

namespace Controller
{
    public class UpgradeStoreController : MonoBehaviour
    {
        public static void Unlock(TowerType.Type type, int level, int price)
        {
            if (SettingHelper.GetAvailableFounds() < price)
            {
                Debug.LogError("Not enough founds");
                return;
            }
            
            SettingHelper.IncreaseSpendsFound(price);
            SettingHelper.SetUnlockedTowerLevel(type, level);
        }

        public static void Unlock(ModifierType.Type type, int level, int price)
        {
            if (SettingHelper.GetAvailableFounds() < price)
            {
                Debug.LogError("Not enough founds");
                return;
            }
            
            SettingHelper.IncreaseSpendsFound(price);
            SettingHelper.SetModifierLevel(type, level);
        }

        public static void LoadSelectLevel()
        {
            SceneManager.LoadScene("Scenes/Select Level Scene");
        }
    }
}
