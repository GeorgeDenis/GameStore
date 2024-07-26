using MediatR;

namespace Application.Features.Reviews.Commands.CreateReviewCommand
{
    public class CreateReviewCommand : IRequest<CreateReviewCommandResponse>
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
    }
}
