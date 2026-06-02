using AutoMapper;
using Microsoft.AspNetCore.OutputCaching;
using Minimal_API_Movies.Repositories;

namespace Minimal_API_Movies.DTOs
{
    public class CreateGenreRequestDTO
    {
        public IOutputCacheStore OutputCacheStore { get; set; } = null!;
        public IGenresRepository GenresRepository { get; set; } = null!;
        public IMapper Mapper { get; set; } = null!;
    }
}
