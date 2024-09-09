using Application.Models.Notification;
using Application.Persistence;
using Mapster;
using MediatR;

namespace Application.Features.Notifications.Queries.GetNotificationsPaginatedQuery
{
    public class GetNotificationsPaginatedQueryHandler : IRequestHandler<GetNotificationsPaginatedQuery, GetNotificationsPaginatedQueryResponse>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ITokenService _tokenService;
        public GetNotificationsPaginatedQueryHandler(INotificationRepository notificationRepository, ITokenService tokenService)
        {
            _notificationRepository = notificationRepository;
            _tokenService = tokenService;
        }
        public async Task<GetNotificationsPaginatedQueryResponse> Handle(GetNotificationsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var userId = _tokenService.GetUserId();
            if (!userId.IsSuccess)
            {
                return new GetNotificationsPaginatedQueryResponse
                {
                    Success = false,
                    Message = "Please use a valid user account"
                };
            }
            var response = await _notificationRepository.GetNotificationsByUserIdPaginated(request.Page, request.Size, Guid.Parse(userId.Value));
            if (!response.IsSuccess)
            {
                return new GetNotificationsPaginatedQueryResponse
                {
                    Success = false,
                    Message = "Something went wrong when retrieving notifications"
                };
            }
            return new GetNotificationsPaginatedQueryResponse
            {
                Success = true,
                Notifications = response.Value,
            };

        }
    }
}
