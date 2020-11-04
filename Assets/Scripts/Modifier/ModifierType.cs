namespace Modifier
{
    public static class ModifierType
    {
        public enum Type
        {
            SLOW_ENEMY,
            DECREASE_DAMAGE_ENEMY
        }

        public static ModifierTarget GetTarget(Type type)
        {
            switch (type)
            {
                case Type.SLOW_ENEMY:
                    return ModifierTarget.ENEMY;
                case Type.DECREASE_DAMAGE_ENEMY:
                    return ModifierTarget.ENEMY;
                default:
                    return ModifierTarget.ENEMY;
            }
        }

        public static ModifierFeature GetFeature(Type type)
        {
            switch (type)
            {
                case Type.SLOW_ENEMY:
                    return ModifierFeature.SPEED;
                case Type.DECREASE_DAMAGE_ENEMY:
                    return ModifierFeature.DAMAGE;
                default:
                    return ModifierFeature.SPEED;
            }
        }
    }
}
