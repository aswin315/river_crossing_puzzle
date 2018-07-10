using RiverPuzzle1.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RiverPuzzle1.Models
{
    public class GameState
    {
        public int ID { get; set; }
        public State State { get; set; }
        public string Reason { get; set; }

        public int GameID { get; set; }

        public Game Game { get; set; }

        [EnsureCollectionSize(4, ErrorMessage ="Invalid character collection")]
        public List<Character> Characters { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var gameStateToVerify = obj as GameState;
            if (ID != gameStateToVerify.ID)
            {
                return false;
            }
            var characterIds = Characters.Select(c => c.ID).ToArray();
            var characterIdsToVerify = gameStateToVerify.Characters
                                            .Select(c => c.ID).ToArray();
            if (!characterIds.SequenceEqual(characterIdsToVerify))
            {
                return false;
            }
            return true;
        }
    }

    public enum State
    {
        InProgress,
        Fail,
        Pass
    }

    
}
