using Application.Common;
using Application.Models.Review;
using FluentValidation;

namespace Application.Validators
{
    public class CreateReviewCommandValidator : AbstractValidator<PostReviewModel>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage(Constants.RatingValue);

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage(Constants.RatingCommentLength);

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired);

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage(Constants.PropertyNameRequired);
        }
    }
}
