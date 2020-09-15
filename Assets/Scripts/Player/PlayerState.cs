using UnityEngine;
using Utils;
using Utils.Messenger;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        private int _defaultHealth;
        private float _defaultReturnRate;

        public int Health => _defaultHealth;
        public float ReturnRate => _defaultReturnRate;

        private void Awake()
        {
            ResourcesHelper.SetFeaturesFromTextFile(
                PlayerFeaturesFile,
                SetFeature
            );
            
            Debug.Log($"initial health: {Health}");

            Messenger<int>.AddListener(GameEvent.ENEMY_REACH_TARGET, OnDamage);
        }

        private void OnDestroy()
        {
            Messenger<int>.RemoveListener(GameEvent.ENEMY_REACH_TARGET, OnDamage);
        }

        private void OnDamage(int damage)
        {
            _defaultHealth -= damage;
            Debug.Log($"remaining health: {Health}");
        }

        private void SetFeature(string featureName, string featureValue)
        {
            switch (featureName)
            {
                case "health":
                    _defaultHealth = int.Parse(featureValue);
                    break;
                case "returnRate":
                    _defaultReturnRate = float.Parse(featureValue);
                    break;
            }
        }

        private const string PlayerFeaturesFile = "Plain/Player/player";
    }
}
