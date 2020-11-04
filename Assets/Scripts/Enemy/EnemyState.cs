using System.Linq;
using Modifier;
using UnityEngine;
using Utils;

namespace Enemy
{
    public class EnemyState : MonoBehaviour
    {
        [SerializeField] private Enemy.Type Type;

        private ModifierManager _modifierManager;

        private float _defaultHealth;
        private float _defaultSpeed;
        private float _defaultTrialDamage;
        private float _defaultCadence;
        private int _defaultHealthDamage;
        private int _defaultCoinDrop;
        private bool _isEngaged;
        public Vector3 targetPosition;
        private float _totalDamage;

        public float Health => _defaultHealth - _totalDamage;
        public float HealthRatio => Health / _defaultHealth;

        public float Speed => _defaultSpeed
                              + _modifierManager.GetModifiers(ModifierTarget.ENEMY, ModifierFeature.SPEED)
                                  .Aggregate(0f, (acc, m) =>
                                      acc + m.Apply(_defaultSpeed)
                                  );

        public float TrialDamage => _defaultTrialDamage;
        public float Cadence => _defaultCadence;

        public int HealthDamage => _defaultHealthDamage
                                   + Mathf.RoundToInt(_modifierManager
                                       .GetModifiers(ModifierTarget.ENEMY, ModifierFeature.DAMAGE)
                                       .Aggregate(0f, (acc, m) =>
                                           acc + m.Apply(_defaultHealthDamage)
                                       )
                                   );

        public int CoindDrop => _defaultCoinDrop;
        public bool IsMoving => !_isEngaged;

        public float DistanceToTarget => (targetPosition - transform.position).magnitude;

        public bool IsEngaged
        {
            get => _isEngaged;
            set => _isEngaged = value;
        }

        public void Awake()
        {
            _modifierManager = FindObjectOfType<ModifierManager>();
            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(EnemyFeaturesFile, Type),
                SetFeature
            );
        }

        public void AddDamage(float damage)
        {
            _totalDamage += damage;
        }

        private void SetFeature(string featureName, string featureValue)
        {
            switch (featureName)
            {
                case "health":
                    _defaultHealth = float.Parse(featureValue);
                    break;
                case "speed":
                    _defaultSpeed = float.Parse(featureValue);
                    break;
                case "trialDamage":
                    _defaultTrialDamage = float.Parse(featureValue);
                    break;
                case "cadence":
                    _defaultCadence = float.Parse(featureValue);
                    break;
                case "healthDamage":
                    _defaultHealthDamage = int.Parse(featureValue);
                    break;
                case "coinDrop":
                    _defaultCoinDrop = int.Parse(featureValue);
                    break;
            }
        }

        private const string EnemyFeaturesFile = "Plain/Enemy/enemy_{0}";
    }
}
