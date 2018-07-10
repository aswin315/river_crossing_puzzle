using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RiverPuzzle1.Data;
using RiverPuzzle1.Models;

namespace RiverPuzzle1.Services
{
    public class GameService : IGameService
    {
        private readonly GameContext _gameContext;
        private readonly IGameStateService _gameStateService;

        public GameService(GameContext gameContext, IGameStateService gameStateService)
        {
            _gameContext = gameContext;
            _gameStateService = gameStateService;
        }

        public async Task<Game> GetGame(int id)
        {
            return await LoadGame(id);
        }

        public IList<Game> GetGames()
        {
            return _gameContext.Games
                .Include(g => g.GameState).ToList();
        }

        public async Task<Game> CreateAsync()
        {
            var game = new Game
            {
                StartTime = DateTime.Now,
                GameState = _gameStateService.Build()
            };

            _gameContext.Games.Add(game);
            await _gameContext.SaveChangesAsync();
            return game;
        }

        public async Task<Game> UpdateAsync(Game game)
        {
            Game gameToUpdate = await LoadGame(game.ID);
            if (gameToUpdate == null)
            {
                return gameToUpdate;
            }

            var gameIsValid = VerifyGameStateBelongsToGame(game, gameToUpdate);
            if (!gameIsValid)
            {
                throw new InvalidOperationException("Not valid game");
            }

            _gameStateService.Update(game.GameState, gameToUpdate.GameState);
            if(gameToUpdate.GameState.State != State.InProgress)
            {
                gameToUpdate.EndTime = DateTime.Now;
                _gameContext.Entry(gameToUpdate).State = EntityState.Modified;
            }
            await _gameContext.SaveChangesAsync();
            return gameToUpdate;
        }

        private async Task<Game> LoadGame(int id)
        {
            return await _gameContext.Games
                                    .Include(g => g.GameState)
                                    .ThenInclude(g => g.Characters)
                                    .SingleOrDefaultAsync(g => g.ID == id);
        }

        private static bool VerifyGameStateBelongsToGame(Game game, Game gameToUpdate)
        {
            var gameState = game.GameState;
            var existingGameState = gameToUpdate.GameState;

            return existingGameState.Equals(gameState);
        }

    }
}
