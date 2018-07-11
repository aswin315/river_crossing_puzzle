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

        public string CanCoexist(object obj)
        {
            var otherCharacter = obj as Character;
            NotCompatibleCharacters.TryGetValue(otherCharacter.Name, out string reason);
            return reason;
        }

        public virtual IDictionary<string, string> NotCompatibleCharacters => new Dictionary<string, string>();
    }

    public enum Location
    {
        LeftBank,
        Boat,
        RightBank
    }
}
