using System;

namespace Enemy
{
    public static class Enemy
    {
        public enum Type: uint
        {
            A, // TODO
            B // TODO
        }

        public static Type GetEnemyType(char type)
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