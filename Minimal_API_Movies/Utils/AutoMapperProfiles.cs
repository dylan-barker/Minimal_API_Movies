using AutoMapper;
using Minimal_API_Movies.DTOs;
using Minimal_API_Movies.Entities;

namespace Minimal_API_Movies.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genre, GenreDTO>();
            CreateMap<CreateGenreDTO, Genre>();

            CreateMap<Actor, ActorDTO>();
            CreateMap<CreateActorDTO, Actor>()
                .ForMember(p => p.Picture, options => options.Ignore());
        }
    }
}
