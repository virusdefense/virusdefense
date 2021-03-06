namespace Board
{
    public static class Block
    {
        public enum Type : uint
        {
            PATH,
            SPAWN,
            DEFENSE,
            BUILD,
            NOT_PLAYABLE,
            SKIP
        }
        
        public static Type GetBlockType(char type)
            {
                switch (type)
                {
                    case 'T': return Type.BUILD;
                    case 'D': return Type.DEFENSE;
                    case 'S': return Type.SPAWN;
                    case 'P': return Type.PATH;
                    case '+': return Type.SKIP;
                }
        
                return Type.NOT_PLAYABLE;
            }
    }
}
