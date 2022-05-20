using AutoMapper;
using WebApi.Entities;
using WebApi.MovieOperations.CreateMovie;
using WebApi.MovieOperations.GetMovies;
using WebApi.MovieOperations.GetMovieDetail;
using WebApi.MovieOperations.UpdateMovie;
using WebApi.ActorOperations.GetActors;
using WebApi.ActorOperations.GetActorDetail;
using WebApi.ActorOperations.UpdateActor;
using WebApi.ActorOperations.CreateActor;
using WebApi.DirectorOperations.GetDirectors;
using WebApi.DirectorOperations.GetDirectorDetail;
using WebApi.DirectorOperations.CreateDirector;
using WebApi.DirectorOperations.UpdateDirector;
using WebApi.UserOperations.Commands.CreateUser;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviesViewModel>().ForMember(d => d.DirectorID, opt => opt.MapFrom(src => src.Director == null ? 0 : src.Director!.Id))
                .ForMember(d => d.ActorIDs, opt => opt.MapFrom(src => src.Actors.Select(x => x.Id)))
                .ForMember(d => d.Date, opt => opt.MapFrom(src => src.Date.ToShortDateString()));
            CreateMap<Movie, MovieDetailModel>().ForMember(d => d.DirectorID, opt => opt.MapFrom(src => src.Director == null ? 0 : src.Director!.Id)).ForMember(d => d.ActorIDs, opt => opt.MapFrom(src => src.Actors.Select(x => x.Id))).ForMember(d => d.Date, opt => opt.MapFrom(src => src.Date.ToShortDateString()));
            CreateMap<CreateMovieModel, Movie>().ForMember(d => d.Actors, opt => opt.Ignore()) //MapFrom(src => src.Actors.Select(x =>x )))
                .ForMember(d => d.DirectorId, opt => opt.MapFrom(src => src.DirectorId));
            CreateMap<UpdateMovieModel, Movie>();
            CreateMap<Actor, ActorsViewModel>().ForMember(d => d.MovieIDs, opt => opt.MapFrom(src => src.Movies.Select(x => x.Id))).ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToShortDateString())); ;
            CreateMap<Actor, ActorDetailModel>().ForMember(d => d.MovieIDs, opt => opt.MapFrom(src => src.Movies.Select(x => x.Id))).ForMember(d => d.BirthDate, opt => opt.MapFrom(src => (src.BirthDate.ToShortDateString())));
            CreateMap<CreateActorModel, Actor>();
            CreateMap<UpdateActorModel, Actor>();
            CreateMap<Director, DirectorsViewModel>().ForMember(d => d.MovieIDs, opt => opt.MapFrom(src => src.Movies.Select(x => x.Id))).ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.BirthDate.ToShortDateString())); ;
            CreateMap<Director, DirectorDetailModel>().ForMember(d => d.MovieIDs, opt => opt.MapFrom(src => src.Movies.Select(x => x.Id))).ForMember(d => d.BirthDate, opt => opt.MapFrom(src => (src.BirthDate.ToShortDateString())));
            CreateMap<CreateDirectorModel, Director>();
            CreateMap<UpdateDirectorModel, Director>();                
            CreateMap<CreateUserModel, User>();
        }

    }
}