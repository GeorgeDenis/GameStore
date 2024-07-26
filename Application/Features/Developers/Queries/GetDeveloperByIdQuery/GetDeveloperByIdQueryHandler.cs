using Application.Models.Developer;
using Application.Persistence;
using Mapster;
using MediatR;

namespace Application.Features.Developers.Queries.GetDeveloperByIdQuery
{
    public class GetDeveloperByIdQueryHandler : IRequestHandler<GetDeveloperByIdQuery, GetDeveloperByIdQueryResponse>
    {
        private readonly IDeveloperRepository developerRepository;
        public GetDeveloperByIdQueryHandler(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }
        public async Task<GetDeveloperByIdQueryResponse> Handle(GetDeveloperByIdQuery request, CancellationToken cancellationToken)
        {
            var developer = await developerRepository.FindByIdAsync(request.DeveloperId);
            if (!developer.IsSuccess)
            {
                return new GetDeveloperByIdQueryResponse
                {
                    Success = false,
                    Message = "Developer with this id not found"
                };
            }
            return new GetDeveloperByIdQueryResponse
            {
                Success = true,
                Developer = developer.Value.Adapt<GetDeveloperModel>()
            };
        }
    }
}
