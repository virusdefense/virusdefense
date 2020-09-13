using System;

namespace Tower
{
    public static class Tower
    {
        public enum Type
        {
            A,
            B
        }

        public static Type GetType(char type)
        {
            switch (type)
            {
                case 'A': return Type.A;
                case 'B': return Type.B;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}