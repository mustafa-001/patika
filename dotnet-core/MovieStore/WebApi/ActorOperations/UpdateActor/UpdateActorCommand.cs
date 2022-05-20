using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;


namespace WebApi.ActorOperations.UpdateActor
{
    public class UpdateActorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private UpdateActorModel UpdateModel;
        public int ActorId;


        public UpdateActorCommand(IMovieStoreDbContext dbContext, IMapper mapper, UpdateActorModel updateModel, int actorId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            UpdateModel = updateModel;
            ActorId = actorId;
        }
        public void Handle()
        {
            var actor = _dbContext.Actors.Include(x=> x.Movies).SingleOrDefault(m => m.Id == ActorId );
            if (actor is null)
            {
                throw new InvalidOperationException("Actor with this Id doesn't exists.");
            }
            if (UpdateModel.MovieIDs.Count != 0)
            {
                //Remove this actor from Actors List of each movie that contains it.
                // foreach(var movie in _dbContext.Movies.Where(x => x.Actors.Select(x=>x.Id).Contains(actor.Id) ).AsEnumerable())
                // {
                //     movie.Actors.Remove(actor);
                // }
                actor.Movies.Clear();
               foreach (int movieId in UpdateModel.MovieIDs)
                {
                    var m = _dbContext.Movies.SingleOrDefault(m => m.Id == movieId);
                    if (m is not null)
                    {
                        actor.Movies.Add(m);
                    }
                    else
                    {
                        throw new InvalidOperationException("Movie with given Id " + movieId + "does not exists.");
                    }
                }
            }
            var newActor = _mapper.Map<Actor>(UpdateModel);
            actor.Name = newActor.Name == String.Empty ? actor.Name : newActor.Name;
            actor.Surname = newActor.Surname == String.Empty ? actor.Surname : newActor.Surname;
            actor.BirthDate = newActor.BirthDate == default ? actor.BirthDate : newActor.BirthDate;
            _dbContext.SaveChanges();

        }
    }
    public class UpdateActorModel
    {
        public string? Name {get; set;} = String.Empty;
        public string? Surname {get; set;} = String.Empty;
        public  List<int> MovieIDs {get; set;} = new List<int>();
        public string? BirthDate {get; set;} = String.Empty;
    }
}