using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RiverPuzzle.Services;
using RiverPuzzle1.Data;
using RiverPuzzle1.Models;

namespace RiverPuzzle1.Services
{
    public class GameStateService : IGameStateService
    {
        private readonly GameContext _gameContext;
        private readonly ICharacterService _characterService;

        public GameStateService(GameContext gameContext, ICharacterService characterService)
        {
            _gameContext = gameContext;
            _characterService = characterService;
        }

        public GameState Build()
        {
            var gameState = new GameState
            {
                State = State.InProgress,
                Characters = BuildInitialCharacters()
            };

            return gameState;
        }

        public void Update(GameState gameState, GameState existingGameState)
        {
            var characters = gameState.Characters;

            var reason = _characterService.Validate(gameState.Characters);
            if (!string.IsNullOrEmpty(reason))
            {
                existingGameState.State = State.Fail;
                existingGameState.Reason = reason;
                _gameContext.Entry(existingGameState).State = EntityState.Modified;
                return;
            }

            UpdateGameState(existingGameState, characters);
        }

        private void UpdateGameState(GameState existingGameState, List<Character> characters)
        {
            var charactersToUpdate = existingGameState.Characters;
            foreach (var character in charactersToUpdate)
            {
                var location = from c in characters
                               where c.ID == character.ID
                               select c.Location;
                character.Location = location.First();
                _gameContext.Entry(character).State = EntityState.Modified;
            }

            var allCharactersCrossed = existingGameState.Characters
                                            .All(c => c.Location == Location.RightBank);

            if (allCharactersCrossed)
            {
                existingGameState.State = State.Pass;
                _gameContext.Entry(existingGameState).State = EntityState.Modified;
            }
        }

        private List<Character> BuildInitialCharacters()
        {
            return new List<Character>
                 {
                     new Farmer() { Location = Location.LeftBank },
                     new Fox() { Location = Location.LeftBank },
                     new Goose() { Location = Location.LeftBank },
                     new Bean() { Location = Location.LeftBank },
                 };
        }
    }
}
