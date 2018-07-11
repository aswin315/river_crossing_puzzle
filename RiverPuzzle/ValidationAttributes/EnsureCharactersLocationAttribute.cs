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
                var validGroup = ValidateCharacterGroup(charactersGroup);
                if(!validGroup)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ValidateCharacterGroup(List<Character> charactersGroup)
        {
            if (charactersGroup.Count == 1 || charactersGroup.Count == 4)
            {
                return true;
            }
            if (charactersGroup.Count == 3)
            {
                return false;
            }
            var firstCharacter = charactersGroup[0];
            var secondCharacter = charactersGroup[1];
            if (!string.IsNullOrEmpty(firstCharacter.CanCoexist(secondCharacter)))
            {
                ErrorMessage = $"{firstCharacter.Name} and {secondCharacter.Name} cannot be in the same location";
                return false;
            }
            return true;
        }
    }
}
