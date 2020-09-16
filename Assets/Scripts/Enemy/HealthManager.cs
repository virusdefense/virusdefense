using UnityEngine;
using UnityEngine.UI;
using Utils.Messenger;

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
                KillEnemy();
        }

        private void KillEnemy()
        {
            Messenger<int>.Broadcast(GameEvent.ENEMY_KILLED, _state.CoindDrop);
            Destroy(gameObject);
        }
    }
}