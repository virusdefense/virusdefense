namespace Modifier
{
    public static class ModifierFactory
    {
        public static Modifier GetModifier(ModifierType.Type type, int level)
        {
            switch (type)
            {
                case ModifierType.Type.SLOW_ENEMY:
                    return GetSlowDownEnemyModifier(level);
                case ModifierType.Type.DECREASE_DAMAGE_ENEMY:
                    return GetSlowDownEnemyModifier(level);
            }

            return null;
        }

        private static Modifier GetSlowDownEnemyModifier(int level)
        {
            switch (level)
            {
                case 1:
                    return Modifier.SlowDownEnemyL1();
                default:
                    return Modifier.SlowDownEnemyL2();
            }
        }

        private static Modifier GetDecreaseDamageEnemy(int level)
        {
            switch (level)
            {
                default:
                    return Modifier.DecreaseEnemyDamageL1();
            }
        }
    }
}
