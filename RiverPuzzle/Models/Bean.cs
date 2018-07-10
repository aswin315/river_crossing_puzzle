using System;
using System.Collections.Generic;

namespace RiverPuzzle1.Models
{
    public class Bean : Character
    {
        public override string Name => "Bean";

        public override IList<string> NotCompatibleCharacters()
        {
            return new List<string>
            {
                "Goose"
            };
        }
    }
}
