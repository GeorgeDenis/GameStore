using Application.Features.Developers.Queries.GetDevelopers;
using Application.Models.Developer;
using Application.Persistence;
using Mapster;
using MediatR;

namespace Application.Features.Developers.Queries.GetDevelopersQuery
{
    public class GetDevelopersQueryHandler : IRequestHandler<GetDevelopersQuery, GetDevelopersQueryResponse>
    {
        private readonly IDeveloperRepository developerRepository;
        public GetDevelopersQueryHandler(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }
        public async Task<GetDevelopersQueryResponse> Handle(GetDevelopersQuery request, CancellationToken cancellationToken)
        {
            var developers = await developerRepository.GetAllAsync();
            return new GetDevelopersQueryResponse
            {
                Developers = developers.Value.Select(x => x.Adapt<GetDeveloperModel>()).ToList(),
            };
        }
    }
}
