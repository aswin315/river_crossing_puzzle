using RiverPuzzle1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverPuzzle1.Services
{
    public interface IGameService
    {
        IList<Game> GetGames();
        Task<Game> GetGame(int id);
        Task<Game> CreateAsync();
        Task<Game> UpdateAsync(Game game);
    }
}
