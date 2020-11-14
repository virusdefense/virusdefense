using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller
{
    public class SelectLevelController : MonoBehaviour
    {
        public static void LoadLevel(int level)
        {
            SceneManager.LoadScene(string.Format(SceneName, level.ToString("00")));
        }

        public static void LoadUpgradeStore()
        {
            SceneManager.LoadScene("Scenes/Store scene");
        }

        private const string SceneName = "Scenes/Levels/Level_{0}";
    }
}
