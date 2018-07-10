using RiverPuzzle1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverPuzzle1.Services
{
    public interface IGameStateService
    {
        GameState Build();
        void Update(GameState gameState, GameState existingGameState);
    }
}
