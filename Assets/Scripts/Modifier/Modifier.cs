namespace Modifier
{
    public class Modifier
    {
        private readonly string _name;
        private readonly ModifierTarget _target;
        private readonly ModifierFeature _feature;
        private int _duration;
        private readonly float _magnitude;

        private Modifier(string name, ModifierTarget target, ModifierFeature feature, int duration, float magnitude)
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

        public static Modifier SlowDownEnemyL1()
        {
            return new Modifier(
                "Slow down enemy level 1",
                ModifierTarget.ENEMY,
                ModifierFeature.SPEED,
                10,
                -40
            );
        }

        public static Modifier SlowDownEnemyL2()
        {
            return new Modifier(
                "Slow down enemy level 2",
                ModifierTarget.ENEMY,
                ModifierFeature.SPEED,
                20,
                -60
            );
        }

        public static Modifier DecreaseEnemyDamageL1()
        {
            return new Modifier(
                "Decrease enemy damage level 1",
                ModifierTarget.ENEMY,
                ModifierFeature.DAMAGE,
                30,
                -50
            );
        }
    }
}
