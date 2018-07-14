using System.Collections.Generic;

namespace RiverPuzzle1.Models
{
    public class Fox : Character
    {
        public override string Name => "Fox";

        public override IDictionary<string, string> NotCompatibleCharacters
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "Goose", "Fox will eat the goose" }
                };
            }
        }
    }
}
