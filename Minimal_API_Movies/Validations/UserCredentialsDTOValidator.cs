using FluentValidation;
using Minimal_API_Movies.DTOs;

namespace Minimal_API_Movies.Validations
{
    public class UserCredentialsDTOValidator: AbstractValidator<UserCredentialsDTO>
    {
        public UserCredentialsDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationUtilities.NotEmptyMessage)
                .MaximumLength(256).WithMessage(ValidationUtilities.MaxLengthMessage)
                .EmailAddress().WithMessage(ValidationUtilities.EmailAddressMessage);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationUtilities.NotEmptyMessage);

        }
    }
}
