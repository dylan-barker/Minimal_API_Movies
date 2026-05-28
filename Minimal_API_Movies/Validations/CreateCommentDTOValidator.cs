using FluentValidation;
using Minimal_API_Movies.DTOs;

namespace Minimal_API_Movies.Validations
{
    public class CreateCommentDTOValidator: AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentDTOValidator()
        {
            RuleFor(x => x.Body)
                .NotEmpty().WithMessage(ValidationUtilities.NotEmptyMessage);
        }
    }
}
