using UnityEngine;
using Utils;
using Utils.Messenger;

namespace Tower
{
    public class TowerState : MonoBehaviour
    {
        [SerializeField] private TowerType.Type type;
        [SerializeField] private int towerLevel;

        private float _defaultRange;
        private float _defaultFireRate;
        private int _defaultPrice;
        private string _warriorType;
        private int _defaultWarriorNumber;
        private float _defaultTrainingTime;
        private float _defaultDamage;
        private int _defaultShootersNumber;

        public float Range => _defaultRange;
        public float FireRate => _defaultFireRate;
        public int Price => _defaultPrice;
        public int WarriorNumber => _defaultWarriorNumber;
        public float TrainingTime => _defaultTrainingTime;
        public float Damage => _defaultDamage;
        public float ShootersNumber => _defaultShootersNumber;
        public TowerType.Type Type => type;
        public int TowerLevel => towerLevel;

        public SpawnTower Block { get; set; }

        public void Awake()
        {
            ResourcesHelper.SetFeaturesFromTextFile(
                string.Format(TowerFeatureFile, type, towerLevel),
                SetFeature
            );

            Messenger<int>.Broadcast(GameEvent.TOWER_CREATED, Price);
        }

        private void SetFeature(string featureName, string featureValue)
        {
            switch (featureName)
            {
                case "price":
                    _defaultPrice = int.Parse(featureValue);
                    break;
                case "warriorType":
                    _warriorType = featureValue; // TODO
                    break;
                case "warriorNumber":
                    _defaultWarriorNumber = int.Parse(featureValue);
                    break;
                case "trainingTime":
                    _defaultTrainingTime = float.Parse(featureValue);
                    break;
                case "fireRate":
                    _defaultFireRate = float.Parse(featureValue);
                    break;
                case "range":
                    _defaultRange = float.Parse(featureValue);
                    break;
                case "damage":
                    _defaultDamage = float.Parse(featureValue);
                    break;
                case "shootersNumber":
                    _defaultShootersNumber = int.Parse(featureValue);
                    break;
            }
        }

        private const string TowerFeatureFile = "Plain/tower/tower_{0}_{1}";
    }
}
