using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Feature : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text valueText;

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetValue(string value)
        {
            valueText.text = value;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}