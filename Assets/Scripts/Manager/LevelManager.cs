using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Messenger;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        private void Awake()
        {
            Messenger.AddListener(GameEvent.RELOAD_SCENE, OnReloadScene);
            Messenger.AddListener(GameEvent.MAIN_MENU,OnLoadMainMenu);
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.RELOAD_SCENE, OnReloadScene);
            Messenger.RemoveListener(GameEvent.MAIN_MENU, OnLoadMainMenu);
        }

        private void OnReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnLoadMainMenu()
        {
            //TODO
            Debug.Log("Load Main Menu");
        }
    }
}