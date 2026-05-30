using FluentValidation;
using Minimal_API_Movies.DTOs;

namespace Minimal_API_Movies.Validations
{
    public class EditClaimDTOValidator : AbstractValidator<EditClaimDTO>
    {
        public EditClaimDTOValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(ValidationUtilities.EmailAddressMessage);
        }
    }
}
