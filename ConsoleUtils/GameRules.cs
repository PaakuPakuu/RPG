using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralUtils
{
    public static class GameRules
    {
        public static readonly string[] CRITERIAS = new string[] { "COU", "INT", "CHA", "ADR", "FOR" };
        public static readonly string[] CRITERIAS_NAME = new string[]
            {
                "Courage",
                "Intelligence",
                "Charisme",
                "Adresse",
                "Force"
            };

        public static readonly int MIN_CRITERIA = 8;
        public static readonly int MAX_CRITERIA = 13;

        public static readonly int TO_ADD_TO_CRITERIA = 7;

        public static readonly int DEFAULT_ATTACK = 8;
        public static readonly int DEFAULT_PARADE = 10;
    }
}
