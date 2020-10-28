using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class SelectLevelUIManager : MonoBehaviour
    {
        [SerializeField] private Button storeButton;

        private void Awake()
        {
            storeButton.onClick.AddListener(() =>
                SceneManager.LoadScene("Scenes/Store scene")
            );
        }
    }
}
