using UnityEngine;

namespace Enemy
{
    public class EnemyState: MonoBehaviour
    {
        [SerializeField] private float defaultHealth;
        [SerializeField] private float defaultSpeed;
        [SerializeField] private float defaultTrialDamage;
        [SerializeField] private float defaultCadence;
        [SerializeField] private int defaultHealthDamage;
        [SerializeField] private int defaultCoinDrop;

        private bool _isEngaged;
        
        

        public float Health => defaultHealth;
        public float Speed => defaultSpeed;
        public float TrialDamage => defaultTrialDamage;
        public float Cadence => defaultCadence;
        public int HealthDamage => defaultHealthDamage;
        public int CoindDrop => defaultCoinDrop;
        public bool IsMoving => !_isEngaged;
        public bool IsEngaged
        {
            get => _isEngaged;
            set => _isEngaged = value;
        }

    }
}