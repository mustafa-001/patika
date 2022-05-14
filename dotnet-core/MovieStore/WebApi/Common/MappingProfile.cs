using AutoMapper;
using WebApi.Entities;
using WebApi.MovieOperations.GetMovies;

namespace WebApi.Common
    {
        public class MappingProfile : Profile
        {
            public MappingProfile(){
                CreateMap<Movie, MoviesViewModel>().ForMember(d => d.Director, opt => opt.MapFrom(src => src.Director.Name+ src.Director.Surname));
                // CreateMap<UpdateMovieModel, Movie>();
            }
        }

    }