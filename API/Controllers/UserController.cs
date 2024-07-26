using Application.Features.Users.Commands.CreateUserCommand;
using Application.Features.Users.Queries.GetUserByIdQuery;
using Application.Features.Users.Queries.GetUsers;
using Application.Models.User;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly SteamContext context;
        public UserController(SteamContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(PostUserModel model)
        {
            var command = new CreateUserCommand
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
            };
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { UserId = id };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetUsersQuery();
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetGamesByUserId(Guid userId)
        //{
        //    var user = await context.Users.Include(c => c.Games).FirstOrDefaultAsync(c => c.UserId == userId);
        //    List<Guid> gameIds = new List<Guid>();
        //    foreach (var game in user.Games)
        //    {
        //        gameIds.Add(game.GameId);
        //    }
        //    return Ok(gameIds);
        //}
        [HttpPost()]
        [Route("game")]
        public async Task<IActionResult> PurchaseGame(Guid userId, Guid gameId)
        {
            var user = await context.Users.Include(c => c.Games).FirstOrDefaultAsync(c => c.UserId == userId);
            if (user == null)
            {
                return BadRequest("User does not exist!");
            }
            var game = await context.Games.FindAsync(gameId);
            if (game == null)
            {
                return BadRequest("Game does not exist!");
            }
            user.Games.Add(game);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
