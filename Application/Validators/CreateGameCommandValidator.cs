using Application.Common;
using Application.Models.Game;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validators
{
    public class CreateGameCommandValidator : AbstractValidator<PostGameModel>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .MaximumLength(100).WithMessage(Constants.PropertyNameMaxLength);

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .MaximumLength(1000).WithMessage(Constants.PropertyNameMaxLength);

            RuleFor(p => p.ReleaseDate)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(Constants.DateValidation);

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage(Constants.GamePrice);

            RuleFor(p => p.DeveloperId)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .NotEqual(Guid.Empty).WithMessage(Constants.GuidValid);
            RuleFor(p => p.Image)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .Must(BeAValidImageFile).WithMessage(Constants.ImageExtension);
        }
        private bool BeAValidImageFile(IFormFile file)
        {
            if(file == null)
            {
                return false;
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".jfif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return !string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension);
        }
    }
}
