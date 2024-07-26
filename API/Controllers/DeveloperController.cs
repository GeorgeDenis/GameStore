using Application.Features.Developers.Commands.CreateDeveloperCommand;
using Application.Features.Developers.Queries.GetDeveloperByIdQuery;
using Application.Features.Developers.Queries.GetDevelopersQuery;
using Application.Models.Developer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DeveloperController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDeveloper(PostDeveloperModel model)
        {
            var command = new CreateDeveloperCommand
            {
                Name = model.Name,
                Description = model.Description,
            };
            var result = await Mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.ValidationsErrors);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeveloperById(Guid id)
        {
            var query = new GetDeveloperByIdQuery { DeveloperId = id};
            var result = await Mediator.Send(query);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<List<GetDeveloperModel>>> GetAll()
        {
            var query = new GetDevelopersQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
