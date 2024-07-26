using Application.Common;
using Application.Models.User;
using FluentValidation;

namespace Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<PostUserModel>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .MaximumLength(100).WithMessage(Constants.PropertyNameMaxLength);
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired)
                .Matches(Constants.EmailRegex)
                .WithMessage(Constants.EmailInvalid);
            RuleFor(x => x.Password)
            .NotEmpty().WithMessage(Constants.PropertyNameRequired)
            .MinimumLength(8)
                .WithMessage(Constants.PasswordLength)
                .Matches(Constants.PasswordUppercaseCharacterRegex)
                .WithMessage(Constants.PasswordUppercase)
                .Matches(Constants.PasswordSpecialCharacterRegex)
                .WithMessage(Constants.PasswordSpecial);
        }
    }
}
