using Application.Features.Games.Commands.DeleteGameCommand;
using Application.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Application.Features.Games.Commands.UpdateGameCommand
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, UpdateGameCommandResponse>
    {
        private readonly IGameRepository gameRepository;

        public UpdateGameCommandHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<UpdateGameCommandResponse> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.FindByIdAsync(request.GameId);
            if (!game.IsSuccess)
            {
                return new UpdateGameCommandResponse
                {
                    Success = false,
                    Message = "Game with this id doesn't exist!",
                };
            }
            var genres = GetGenres(request.Genres);
            byte[] image = null;
            if (request.Image != null && request.Image.Length > 0)
            {
                image = await ConvertFormFileToByteArray(request.Image);
            }

            image ??= game.Value.Image;
            var response = game.Value.Update(request.Name, request.Description, genres, request.Price, image);
            if (!response.IsSuccess)
            {
                return new UpdateGameCommandResponse
                {
                    Success = false,
                    ValidationsErrors = [response.Error]
                };
            }
            var updateResponse = await gameRepository.UpdateAsync(response.Value);
            if (!response.IsSuccess)
            {
                return new UpdateGameCommandResponse
                {
                    Success = false,
                    Message = "Update for this game failed!"
                };
            }
            return new UpdateGameCommandResponse
            {
                Success = true,
            };
        }
        public static async Task<byte[]> ConvertFormFileToByteArray(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }
            using (var memoryStream = new MemoryStream())
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
