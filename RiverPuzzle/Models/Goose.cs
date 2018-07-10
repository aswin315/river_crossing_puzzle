using System.Collections.Generic;

namespace RiverPuzzle1.Models
{
    public class Goose : Character
    {
        public override string Name => "Goose";

        public override IList<string> NotCompatibleCharacters()
        {
            return new List<string>
            {
                "Fox",
                "Bean"
            };
        }
    }
}
