using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Enemy
{
    public class HealthManager : MonoBehaviour
    {
        private EnemyState _state;
        public Image healthBar;

        private void Awake()
        {
            _state = GetComponent<EnemyState>();
        }

        private void Update()
        {
            healthBar.fillAmount = _state.HealthRatio;

            if (_state.Health <= 0)
                Destroy(gameObject);
        }
    }
}