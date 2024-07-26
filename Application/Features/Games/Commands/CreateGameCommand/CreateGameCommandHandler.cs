using Application.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Application.Features.Games.Commands.CreateGameCommand
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, CreateGameCommandResponse>
    {
        private readonly IGameRepository gameRepository;
        private readonly IDeveloperRepository developerRepository;
        public CreateGameCommandHandler(IGameRepository gameRepository, IDeveloperRepository developerRepository)
        {
            this.gameRepository = gameRepository;
            this.developerRepository = developerRepository;
        }
        public async Task<CreateGameCommandResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var developer = await developerRepository.FindByIdAsync(request.DeveloperId);
            if (!developer.IsSuccess)
            {
                return new CreateGameCommandResponse
                {
                    Success = false,
                    Message = "Developer with this id doesn't exist"
                };
            }
            var image = await ConvertFormFileToByteArray(request.Image);
            var genre = GetGenres(request.Genres); 
            var game = new Game(request.Name, request.Description, request.ReleaseDate, genre, request.Price, request.DeveloperId, image);
            var result = await gameRepository.AddAsync(game);
            if (!result.IsSuccess)
            {
                return new CreateGameCommandResponse
                {
                    Success = false,
                    Message = "Something went wrong while posting the game!"
                };

            }
            return new CreateGameCommandResponse
            {
                Success = true,
            };
        }
        public static async Task<byte[]> ConvertFormFileToByteArray(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return null;
            }
            using(var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public static string GetGenres(IEnumerable<string> genres)
        {
            var genre = new StringBuilder();
            foreach (var genresItem in genres)
            {
                genre.Append(genresItem);
                genre.Append(',');
            }
            genre.Remove(genre.Length - 1, 1);
            return genre.ToString();
        }
    }
}
