using FluentValidation;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Repositories;

namespace Minimal_API_Movies.Validations
{
    public class CreateGenreDTOValidator : AbstractValidator<CreateGenreDTO>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CreateGenreDTOValidator(IGenresRepository genresRepository, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            var routeValueId = httpContextAccessor.HttpContext!.Request.RouteValues["id"];
            var id = 0;

            if (routeValueId is string routeValueIdString)
            {
                int.TryParse(routeValueIdString, out id);
            }

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationUtilities.NotEmptyMessage)
                .MaximumLength(150).WithMessage(ValidationUtilities.MaxLengthMessage)
                .Must(ValidationUtilities.FirstLetterIsUppercase).WithMessage(ValidationUtilities.FirstLetterUppercaseMessage)
                .MustAsync(async (name, _) =>
                {
                    var exists = await genresRepository.Exists(id, name);
                    return !exists;
                }).WithMessage(g => $"{g.Name} is already in use.");
        }

        
    }
}
