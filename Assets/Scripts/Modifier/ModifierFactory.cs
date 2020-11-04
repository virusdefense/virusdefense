namespace Modifier
{
    public class ModifierFactory
    {
        public static Modifier GetModifier(ModifierType type)
        {
            switch (type)
            {
                case ModifierType.SLOW_ENEMY_L1:
                    return Modifier.SlowDownEnemyL1();
                case ModifierType.SLOW_ENEMY_L2:
                    return Modifier.SlowDownEnemyL2();
                case ModifierType.DECREASE_DAMAGE_ENEMY_L1:
                    return Modifier.DecreaseEnemyDamageL1();
            }

            return null;
        }
    }
}
