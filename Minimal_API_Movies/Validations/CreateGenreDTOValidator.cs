using FluentValidation;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Repositories;

namespace Minimal_API_Movies.Validations
{
    public class CreateGenreDTOValidator : AbstractValidator<CreateGenreDTO>
    {
        public CreateGenreDTOValidator(IGenresRepository genresRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(150).WithMessage("{PropertyName} cannot exceed 150 characters.")
                .Must(FirstLetterIsUppercase).WithMessage("{PropertyName} must start with an uppercase letter.")
                .MustAsync(async (name, _) =>
                {
                    var exists = await genresRepository.Exists(id:0, name);
                    return !exists;
                }).WithMessage(g => $"{g.Name} is already in use.");
        }

        private bool FirstLetterIsUppercase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return true;
            var firstLetter = value[0].ToString();
            return firstLetter == firstLetter.ToUpper();
        }
    }
}
