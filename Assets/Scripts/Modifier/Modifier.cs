namespace Modifier
{
    public class Modifier
    {
        private readonly string _name;
        private readonly ModifierTarget _target;
        private readonly ModifierFeature _feature;
        private int _duration;
        private readonly float _magnitude;

        public Modifier(string name, ModifierTarget target, ModifierFeature feature, int duration, float magnitude)
        {
            _name = name;
            _target = target;
            _feature = feature;
            _duration = duration;
            _magnitude = magnitude;
        }

        public ModifierTarget Target => _target;

        public ModifierFeature Feature => _feature;

        public string Name => _name;

        public bool IsExpired => _duration <= 0;

        public void Countdown(int deltaTime)
        {
            _duration -= deltaTime;
        }

        public float Applay(float featureValue)
        {
            return featureValue * _magnitude;
        }
    }
}
