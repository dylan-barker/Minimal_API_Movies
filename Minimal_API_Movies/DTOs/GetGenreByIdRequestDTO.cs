using AutoMapper;
using Minimal_API_Movies.Repositories;

namespace Minimal_API_Movies.DTOs
{
    public class GetGenreByIdRequestDTO
    {
        public IGenresRepository Repository { get; set; } = null!;
        public int Id { get; set; }
        public IMapper Mapper { get; set; } = null!;
    }
}
