using Application.Models.Developer;
using Application.Responses;

namespace Application.Features.Developers.Queries.GetDevelopers
{
    public class GetDevelopersQueryResponse : BaseResponse
    {
        public GetDevelopersQueryResponse() : base() { }
        public List<GetDeveloperModel> Developers { get; set; } = [];
    }
}