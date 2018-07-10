using RiverPuzzle1.Models;

namespace RiverPuzzle.Factories
{
    public static class CharacterFactory
    {
        public static Character Build(string name)
        {
            switch (name)
            {
                case "Farmer":
                    return new Farmer();
                case "Fox":
                    return new Fox();
                case "Bean":
                    return new Bean();
                case "Goose":
                    return new Goose();
                default:
                    return null;
            }
        }
    }
}
