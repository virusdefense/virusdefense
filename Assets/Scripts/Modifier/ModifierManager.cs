using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using Utils.Messenger;
using Utils.Settings;

namespace Modifier
{
    public class ModifierManager : MonoBehaviour
    {
        private PlayerState _playerState;
        
        private readonly List<Modifier> _modifiersList = new List<Modifier>();
        private readonly Dictionary<ModifierTarget, List<Modifier>> _modifiersMap =
            new Dictionary<ModifierTarget, List<Modifier>>();

        private void Awake()
        {
            _playerState = FindObjectOfType<PlayerState>();

            Messenger<ModifierType>.AddListener(GameEvent.MODIFIER_USED, OnModifierUsed);
        }

        private void OnDestroy()
        {
            Messenger<ModifierType>.RemoveListener(GameEvent.MODIFIER_USED, OnModifierUsed);
        }

        private void Update()
        {
            _modifiersList.ForEach(m => m.Countdown(Time.deltaTime));
            _modifiersList.Where(m => m.IsExpired)
                .ToList()
                .ForEach(RemoveModifier);
        }

        public List<Modifier> GetModifiers(ModifierTarget target, ModifierFeature feature)
        {
            try
            {
                return _modifiersMap[target]
                    .Where(m => m.Feature == feature)
                    .ToList();
            }
            catch (Exception e)
            {
                return new List<Modifier>();
            }
        }

        private void OnModifierUsed(ModifierType type)
        {
            if (!_playerState.IsModifierAvailable(type)) return;

            var level = SettingHelper.GetModifierLevel(type)
                .GetOrDefault(0);
            if (level == 0) return;

            var modifier = ModifierFactory.GetModifier(type, level);

            _modifiersList.Add(modifier);

            List<Modifier> modifiers;
            try
            {
                modifiers = _modifiersMap[modifier.Target];
            }
            catch (Exception e)
            {
                modifiers = new List<Modifier>();
                _modifiersMap[modifier.Target] = modifiers;
            }

            modifiers.Add(modifier);

            _playerState.DecreaseModifierUnit(type);
        }

        private void RemoveModifier(Modifier modifier)
        {
            var modifiers = _modifiersMap[modifier.Target];
            modifiers?.Remove(modifier);
            _modifiersList.Remove(modifier);
        }
    }
}