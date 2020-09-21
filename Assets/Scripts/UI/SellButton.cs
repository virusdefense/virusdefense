using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SellButton : MonoBehaviour
    {
        private Text _buttonText;
        private Button _button;
        private PlayerState _playerState;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _buttonText = GetComponentInChildren<Text>();
            _playerState = FindObjectOfType<PlayerState>();
        }

        public void UpdateButton(int originalPrice)
        {
            var moneyBack = Mathf.FloorToInt(originalPrice * _playerState.ReturnRate);
            _buttonText.text = $"<b>Sell</b>\n{moneyBack}";
        }
    }
}