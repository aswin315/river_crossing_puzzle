using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiverPuzzle1.Models;
using RiverPuzzle1.Services;

namespace RiverPuzzle1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]

    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/Games
        [HttpGet]
        public IEnumerable<Game> GetGames()
        {
            return _gameService.GetGames();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _gameService.GetGame(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame([FromRoute] int id, [FromBody]Game game)
        {
            Game updatedGame = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                updatedGame = await _gameService.UpdateAsync(game);
                if (updatedGame == null)
                {
                    return BadRequest();
                }
            }
            catch(InvalidOperationException invalid)
            {
                return BadRequest(invalid.Message);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
            return Ok(updatedGame);
        }

        // POST: api/Games
        [HttpPost]
        public async Task<IActionResult> PostGame()
        {
            var game = await _gameService.CreateAsync();
       
            return CreatedAtAction("GetGame", new { id = game.ID }, game);
        }
    }
}