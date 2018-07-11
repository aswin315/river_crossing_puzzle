using System;
using System.Collections.Generic;

namespace RiverPuzzle1.Models
{
    public class Bean : Character
    {
        public override string Name => "Bean";

        public override IDictionary<string, string> NotCompatibleCharacters
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "Goose", "Goose will eat the bean" }
                };
            }
        }
    }
}
