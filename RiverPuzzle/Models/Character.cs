using Newtonsoft.Json;
using RiverPuzzle1.JsonConverters;
using System.Collections.Generic;

namespace RiverPuzzle1.Models
{

    [JsonConverter(typeof(CharacterJsonConverter))]
    public class Character
    {
        public int ID { get; set; }
        public virtual string Name { get; set; }
        public Location Location { get; set; }
        public int GameStateId { get; set; }

        public GameState GameState { get; set; }

        public virtual bool CanCoexist(object obj)
        {
            var otherCharacter = obj as Character;
            var isNotCompatible = NotCompatibleCharacters().Contains(otherCharacter.Name);
            if (isNotCompatible)
            {
                return false;
            }
            return true;
        }

        public virtual IList<string> NotCompatibleCharacters()
        {
            return new List<string>();
        }
    }

    public enum Location
    {
        LeftBank,
        Boat,
        RightBank
    }
}
