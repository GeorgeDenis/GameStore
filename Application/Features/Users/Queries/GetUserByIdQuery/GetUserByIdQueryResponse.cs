using Application.Models.User;
using Application.Responses;

namespace Application.Features.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQueryResponse : BaseResponse
    {
        public GetUserByIdQueryResponse() : base()
        {
            
        }
        public GetUserModel User { get; set; }
    }
}