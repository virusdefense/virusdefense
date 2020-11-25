using Modifier;
using Tower;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Messenger;
using Utils.Settings;

namespace Controller
{
    public class UpgradeStoreController : MonoBehaviour
    {
        public static void Unlock(TowerType.Type type, int level, int price)
        {
            SettingHelper.IncreaseSpendsFound(price);
            SettingHelper.SetUnlockedTowerLevel(type, level);

            Messenger.Broadcast(GameEvent.UPGRADE_PURCHASED);
        }

        public static void Unlock(ModifierType.Type type, int level, int price)
        {
            SettingHelper.IncreaseSpendsFound(price);
            SettingHelper.SetModifierLevel(type, level);

            Messenger.Broadcast(GameEvent.UPGRADE_PURCHASED);
        }

        public static void LoadSelectLevel()
        {
            SceneManager.LoadScene("Scenes/Select Level Scene");
        }
    }
}
