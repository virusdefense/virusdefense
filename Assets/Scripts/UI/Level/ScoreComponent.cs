using UnityEngine;
using UnityEngine.UI;

namespace UI.Level
{
    public class ScoreComponent : MonoBehaviour
    {
        [SerializeField] private Image bar;
        [SerializeField] private Text multiplier;

        private float _currentFill;
        private float _targetFill;
        private int _mulValue;

        public float TargetFill
        {
            set => _targetFill = value;
        }

        public int MultiplierValue
        {
            set => _mulValue = value;
        }

        private void Awake()
        {
            bar.fillAmount = 0;
        }

        private void Update()
        {
            multiplier.text = $"x{_mulValue}";

            if (_currentFill >= _targetFill) return;

            _currentFill += Time.deltaTime * FillSpeed;
            bar.fillAmount = _currentFill > _targetFill ? _targetFill : _currentFill;
        }

        private const float FillSpeed = 0.5f;
    }
}
