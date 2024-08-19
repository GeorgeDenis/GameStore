using Application.Features.GameUser.Commands.AddGameUserCommand;
using Application.Features.GameUser.Queries.GetGamesByUserIdQuery;
using Application.Features.GameUser.Queries.GetPurchaseStatus;
using Application.Models.GameUser;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class GameUserController : ApiControllerBase
    {
        private readonly SteamContext context;
        public GameUserController(SteamContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Purchase([FromBody] PurchaseRequest model)
        {
            var command = new AddGameUserCommand { GameId = model.GameId, UserId = model.UserId };
            var result = await Mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetGamesByUserId()
        {
            //var user = await context.Users.Include(c => c.Games).FirstOrDefaultAsync(c => c.UserId == userId);
            //List<GameDto> games = new List<GameDto>();
            //foreach(var game in user.Games)
            //{
            //    games.Add(new GameDto
            //    {
            //        GameId = game.GameId,
            //        Name = game.Name,
            //        Description = game.Description,
            //        ReleaseDate = game.ReleaseDate,
            //        Genre = game.Genre,
            //        Price = game.Price
            //    });
            //}
            //return Ok(games);
            var command = new GetGamesByUserIdQuery { };
            var result = await Mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("purchase/{gameId}")]
        public async Task<IActionResult> GetPurchaseStatus(Guid gameId)
        {
            var query = new GetPurchaseStatusQuery { GameId = gameId };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
