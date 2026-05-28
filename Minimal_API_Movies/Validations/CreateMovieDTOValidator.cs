using FluentValidation;
using Minimal_API_Movies.DTOs;

namespace Minimal_API_Movies.Validations
{
    public class CreateMovieDTOValidator : AbstractValidator<CreateMovieDTO>
    {
        public CreateMovieDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationUtilities.NotEmptyMessage)
                .MaximumLength(100).WithMessage(ValidationUtilities.MaxLengthMessage);
        }
    }
}
