using System.Linq;
using Modifier;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utils.Messenger;

namespace UI.HUD.Modifiers
{
    public class ModifiersButton : MonoBehaviour
    {
        [SerializeField] private ModifierType.Type type;
        private Button _button;
        private PlayerState _playerState;
        private ModifierManager _modifierManager;
        private ModifierTarget _target;
        private ModifierFeature _feature;

        private void Awake()
        {
            _playerState = FindObjectOfType<PlayerState>();
            _modifierManager = FindObjectOfType<ModifierManager>();

            GetComponentInChildren<Text>().text = type.ToString();

            _button = GetComponent<Button>();

            _button.onClick.AddListener(() =>
                    Messenger<ModifierType.Type>.Broadcast(GameEvent.MODIFIER_USED, type)
            );

            _target = ModifierType.GetTarget(type);
            _feature = ModifierType.GetFeature(type);
        }

        private void Update()
        {
            var modifiers = _modifierManager.GetModifiers(_target, _feature);
            
            if (modifiers.Count > 0)
            {
                _button.interactable = false;
                _button.image.fillAmount = modifiers.First().RemainingTimeRatio;
            }
            else
            {
                _button.image.fillAmount = 1;
                _button.interactable = _playerState.IsModifierAvailable(type);
            }
        }
    }
}