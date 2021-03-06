using Manager;
using Modifier;
using UnityEngine;
using Utils;
using Utils.Messenger;
using Utils.Settings;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] public int level;

        private int _defaultHealth;
        private float _defaultReturnRate;
        private int _coin;
        private int _initialCoin;
        private int _totalDamage;
        private int _unlockNext;
        private int _slowDownEnemyUnit;
        private GameManager _gameManager;

        public int Health => _defaultHealth - _totalDamage;
        public float HealthRatio => (float) Health / _defaultHealth;
        public float CoinRation => _coin > _initialCoin ? 1 : (float) _coin / _initialCoin;
        public float ReturnRate => _defaultReturnRate;
        public int Coin => _coin;
        public int Level => level;
        public int UnlockNext => _unlockNext;

        private void Update()
        {
            Debug.Log($"Unit {_slowDownEnemyUnit}");
        }

        private void Awake()
        {
            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(PlayerFeaturesFile, level.ToString("00")),
                SetFeature
            );

            _initialCoin = _coin;

            _gameManager = FindObjectOfType<GameManager>();

            Debug.Log($"initial health: {Health}");

            Messenger<int>.AddListener(GameEvent.ENEMY_REACH_TARGET, OnDamage);
            Messenger<int>.AddListener(GameEvent.ENEMY_KILLED, OnEnemyKilled);
            Messenger<int>.AddListener(GameEvent.TOWER_CREATED, OnTowerCreated);
            Messenger<int>.AddListener(GameEvent.TOWER_SELLED, OnTowerSelled);

            Debug.Log($"isLevelCompleted: {SettingHelper.IsLevelCompleted(level).GetOrDefault(false)}");
            Debug.Log($"levelScore: {SettingHelper.GetLevelScore(level).GetOrDefault(0)}");
        }

        private void OnDestroy()
        {
            Messenger<int>.RemoveListener(GameEvent.ENEMY_REACH_TARGET, OnDamage);
            Messenger<int>.RemoveListener(GameEvent.ENEMY_KILLED, OnEnemyKilled);
            Messenger<int>.RemoveListener(GameEvent.TOWER_CREATED, OnTowerCreated);
            Messenger<int>.RemoveListener(GameEvent.TOWER_SELLED, OnTowerSelled);
        }

        private void LateUpdate()
        {
            if (Health <= 0 && _gameManager.IsGameOnPlay)
                Messenger.Broadcast(GameEvent.OVER);
        }

        public bool IsModifierAvailable(ModifierType.Type type)
        {
            return SettingHelper.GetModifierLevel(type)
                .GetOrDefault(0) > 0
                && GetModifierUnit(type) > 0;
        }

        private int GetModifierUnit(ModifierType.Type type)
        {
            switch (type)
            {
                case ModifierType.Type.SLOW_ENEMY:
                    return _slowDownEnemyUnit;
                default:
                    return 0;
            }
        }

        public void DecreaseModifierUnit(ModifierType.Type type)
        {
            switch (type)
            {
              case  ModifierType.Type.SLOW_ENEMY:
                  _slowDownEnemyUnit--;
                  break;
            }
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
            Messenger.Broadcast(GameEvent.COIN_CHANGE);
        }

        private void OnTowerCreated(int price)
        {
            _coin -= price;
            Messenger.Broadcast(GameEvent.COIN_CHANGE);
        }

        private void OnTowerSelled(int moneyBack)
        {
            _coin += moneyBack;
            Messenger.Broadcast(GameEvent.COIN_CHANGE);
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
                case "coin":
                    _coin = int.Parse(featureValue);
                    break;
                case "unlockNext":
                    _unlockNext = int.Parse(featureValue);
                    break;
                case "slowDownEnemy":
                    _slowDownEnemyUnit = int.Parse(featureValue);
                    break;
            }
        }

        private const string PlayerFeaturesFile = "Plain/Player/level_{0}";
    }
}
