using UnityEngine;
using Utils;
using Utils.Messenger;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] public int level;

        private int _defaultHealth;
        private float _defaultReturnRate;
        private int _coin;
        private int _totalDamage;

        public int Health => _defaultHealth - _totalDamage;
        public float HealthRatio => Health / _defaultHealth;
        public float ReturnRate => _defaultReturnRate;

        private void Awake()
        {
            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(PlayerFeaturesFile, level.ToString("00")),
                SetFeature
            );

            Debug.Log($"initial health: {Health}");

            Messenger<int>.AddListener(GameEvent.ENEMY_REACH_TARGET, OnDamage);
            Messenger<int>.AddListener(GameEvent.ENEMY_KILLED, OnEnemyKilled);
        }

        private void OnDestroy()
        {
            Messenger<int>.RemoveListener(GameEvent.ENEMY_REACH_TARGET, OnDamage);
            Messenger<int>.RemoveListener(GameEvent.ENEMY_KILLED, OnEnemyKilled);
        }

        private void OnDamage(int damage)
        {
            _totalDamage += damage;
            Debug.Log($"remaining health: {Health}");
        }

        private void OnEnemyKilled(int coinDrop)
        {
            _coin += coinDrop;
            Debug.Log($"coin: {_coin}");
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

        private const string PlayerFeaturesFile = "Plain/Player/level_{0}";
    }
}
