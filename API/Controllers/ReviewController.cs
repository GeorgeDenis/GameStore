using Application.Features.Reviews.Commands.CreateReviewCommand;
using Application.Features.Reviews.Query.GetReviewByIdQuery;
using Application.Features.Reviews.Query.GetReviewsByGameIdQuery;
using Application.Features.Reviews.Query.GetReviewsQuery;
using Application.Models.Review;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ReviewController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateReview(PostReviewModel model)
        {
            var command = model.Adapt<CreateReviewCommand>();
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            var query = new GetReviewByIdQuery { ReviewId = id };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var query = new GetReviewsQuery();
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("games/{gameId}")]
        public async Task<IActionResult> GetReviewsByGameId(Guid gameId)
        {
            var query = new GetReviewsByGameIdQuery { GameId = gameId };
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
