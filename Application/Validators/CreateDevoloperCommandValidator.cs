using Application.Common;
using Application.Features.Developers.Commands.CreateDeveloperCommand;
using Application.Models.Developer;
using FluentValidation;

namespace Application.Validators
{
    public class CreateDevoloperCommandValidator : AbstractValidator<PostDeveloperModel>
    {
        public CreateDevoloperCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage(Constants.PropertyNameRequired);
            RuleFor(p => p.Description).NotEmpty();
        }
    }
}
