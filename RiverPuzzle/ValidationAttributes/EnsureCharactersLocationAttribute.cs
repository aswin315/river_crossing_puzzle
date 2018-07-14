using RiverPuzzle1.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RiverPuzzle1.ValidationAttributes
{
    public class EnsureCharactersLocationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var characters = value as IList<Character>;
            var charactersBasedOnLocation = characters
                                  .GroupBy(l => l.Location)
                                  .Select(g => g.ToList())
                                  .ToList();

            foreach(var charactersGroup in charactersBasedOnLocation )
            {
                var reason = ValidateCharacterGroup(charactersGroup);
                if(!string.IsNullOrEmpty(reason))
                {
                    ErrorMessage = reason;
                    return false;
                }
            }
            return true;
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
            var firstCharacter = charactersGroup[0];
            var secondCharacter = charactersGroup[1];
            return firstCharacter.CanCoexist(secondCharacter);
        }
    }
}
