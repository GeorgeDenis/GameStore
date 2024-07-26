using Application.Models.Developer;
using Application.Responses;

namespace Application.Features.Developers.Queries.GetDeveloperByIdQuery
{
    public class GetDeveloperByIdQueryResponse : BaseResponse
    {
        public GetDeveloperByIdQueryResponse() : base() { }
        public GetDeveloperModel Developer { get; set; }
    }
}
