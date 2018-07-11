using System.Collections.Generic;

namespace RiverPuzzle1.Models
{
    public class Goose : Character
    {
        public override string Name => "Goose";

        public override IDictionary<string, string> NotCompatibleCharacters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "Fox", "Fox will eat the goose" },
                    { "Bean", "Goose will eat the bean" }
                };
            }
        }
    }
}
