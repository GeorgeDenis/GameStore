using Application.Models.User;
using Application.Responses;

namespace Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryResponse : BaseResponse
    {
        public GetUsersQueryResponse() : base() { }
        public List<GetUserModel> Users { get; set; }
    }
}