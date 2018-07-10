using System.Collections.Generic;

namespace RiverPuzzle1.Models
{
    public class Fox : Character
    {
        public override string Name => "Fox";

        public override IList<string> NotCompatibleCharacters()
        {
            return new List<string>
            {
                "Goose"
            };
        }
    }
}
