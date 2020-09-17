using System;

namespace Tower
{
    public static class TowerType
    {
        public enum Type
        {
            GROUND,
            AIR_LIGHT,
            AIR_HEAVY
        }

        public static Type GetType(char type)
        {
            switch (type)
            {
                case 'G': return Type.GROUND;
                case 'L': return Type.AIR_LIGHT;
                case 'H': return Type.AIR_HEAVY;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}