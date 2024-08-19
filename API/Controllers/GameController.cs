using API.Attributes;
using Application.Features.Games.Commands.CreateGameCommand;
using Application.Features.Games.Commands.DeleteGameCommand;
using Application.Features.Games.Commands.UpdateGameCommand;
using Application.Features.Games.Queries.GetGameByIdQuery;
using Application.Features.Games.Queries.GetGamesQuery;
using Application.Features.GameUser.Queries.GetPurchaseStatus;
using Application.Models.Game;
using Application.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GameController : ApiControllerBase
    {
        private readonly IAppLogger<GameController> _logger;

        public GameController(IAppLogger<GameController> logger) {
            _logger = logger;
        }
        [Authentication]
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromForm] PostGameModel model)
        {
            var command = new CreateGameCommand
            {
                Name = model.Name,
                Description = model.Description,
                ReleaseDate = model.ReleaseDate,
                Price = model.Price,
                DeveloperId = model.DeveloperId,
                Image = model.Image,
                Genres = model.Genres
            };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [Authentication]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var command = new DeleteGameCommand { GameId = id };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGame([FromForm] UpdateGameModel model)
        {
            var command = new UpdateGameCommand
            {
                GameId = model.GameId,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price, 
                Image = model.Image,
                Genres = model.Genres
            };

            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            var query = new GetGameByIdQuery { GameId = id };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var query = new GetGamesQuery();
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            _logger.LogInformation("This is a TRACE log for testing.");
            _logger.LogDebug("This is a DEBUG log for testing.");

            _logger.LogInformation("Get all games!");
            return Ok(result);
        }


    }
}
