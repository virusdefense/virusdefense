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
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.RELOAD_SCENE, OnReloadScene);
        }

        private void OnReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}