using FluentValidation;
using Minimal_API_Movies.DTOs;

namespace Minimal_API_Movies.Validations
{
    public class CreateActorDTOValidator: AbstractValidator<CreateActorDTO>
    {
        public CreateActorDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationUtilities.NotEmptyMessage)
                .MaximumLength(150).WithMessage(ValidationUtilities.MaxLengthMessage);

            var minimumDate = new DateTime(1900, 1, 1);

            RuleFor(p => p.DateOfBirth)
                .GreaterThanOrEqualTo(minimumDate).WithMessage(ValidationUtilities.GreaterThanDateMessage(minimumDate));
        }
    }
}
