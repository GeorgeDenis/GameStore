using Application.Common;
using Application.Models.Game;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Application.Validators
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameModel>
    {
        public UpdateGameCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .MaximumLength(100).WithMessage(Constants.PropertyNameMaxLength);
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .MaximumLength(1000).WithMessage(Constants.PropertyNameMaxLength);
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage(Constants.GamePrice);

            RuleFor(x => x.Image).NotEmpty().When(x => x.Image != null).WithMessage("Image cannot be empty if provided.")
                .Must(BeAValidImageFile).When(x => x.Image != null).WithMessage("Image must be a valid format.");
        }
        private bool BeAValidImageFile(IFormFile file)
        {
            if (file == null)
            {
                return false;
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".jfif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return !string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension);
        }
    }
}
