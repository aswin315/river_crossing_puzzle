using System.Collections.Generic;
using System.Linq;
using RiverPuzzle1.Models;

namespace RiverPuzzle.Services
{
    public class CharacterService : ICharacterService
    {
        public string Validate(List<Character> characters)
        {
            var charactersBasedOnLocation = characters
                                 .GroupBy(l => l.Location)
                                 .Select(g => g.ToList())
                                 .ToList();

            foreach (var charactersGroup in charactersBasedOnLocation)
            {
                var reason = ValidateCharacterGroup(charactersGroup);
                if (!string.IsNullOrEmpty(reason))
                {
                    return reason;
                }
            }
            return string.Empty;
        }

        private string ValidateCharacterGroup(List<Character> charactersGroup)
        {
            if (charactersGroup.Count == 1 || charactersGroup.Count == 4)
            {
                return string.Empty;
            }
            if (charactersGroup.Count == 3)
            {
                return "Too many characters in the same location";
            }

            var firstCharacterName = charactersGroup[0].Name;
            var secondCharacterName = charactersGroup[1].Name;
            return ValidateCharacters(firstCharacterName, secondCharacterName);
        }

        private string ValidateCharacters(string firstCharacterName, string secondCharacterName)
        {
            switch (firstCharacterName)
            {
                case "Fox":
                    if (secondCharacterName == "Goose")
                    {
                        return "Fox will eat the Goose";
                    }
                    break;
                case "Goose":
                    if (secondCharacterName == "Fox")
                    {
                        return "Fox will eat the Goose";
                    }
                    else if (secondCharacterName == "Bean")
                    {
                        return "Goose will eat the Bean";
                    }
                    break;
                case "Bean":
                    if (secondCharacterName == "Goose")
                    {
                        return "Goose will eat the Bean";
                    }
                    break;
            }
            return string.Empty;
        }
    }
}
