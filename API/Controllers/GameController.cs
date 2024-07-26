using Application.Features.Games.Commands.CreateGameCommand;
using Application.Features.Games.Queries.GetGameByIdQuery;
using Application.Features.Games.Queries.GetGamesQuery;
using Application.Models.Game;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GameController : ApiControllerBase
    {
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
            return Ok(result);
        }
    }
}
