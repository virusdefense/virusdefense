using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class EnemyState : MonoBehaviour
    {
        [SerializeField] private EnemyType Type;

        private float _defaultHealth;
        private float _defaultSpeed;
        private float _defaultTrialDamage;
        private float _defaultCadence;
        private int _defaultHealthDamage;
        private int _defaultCoinDrop;
        private bool _isEngaged;
        public Vector3 targetPosition;

        public float Health => _defaultHealth;
        public float Speed => _defaultSpeed;
        public float TrialDamage => _defaultTrialDamage;
        public float Cadence => _defaultCadence;
        public int HealthDamage => _defaultHealthDamage;
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
            Resources.Load<TextAsset>(string.Format(EnemyFeaturesFile, Type))
                .text.Split('\n')
                .Select(line => line.Split(' ')).ToList()
                .ForEach(token => SetFeature(token[0], token[1]));
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