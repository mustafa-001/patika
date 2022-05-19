using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.MovieOperations.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private UpdateMovieModel UpdateModel;
        public int MovieId;


        public UpdateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper, UpdateMovieModel updateModel, int movieId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            UpdateModel = updateModel;
            MovieId = movieId;
        }
        public void Handle()
        {
            var movie = _dbContext.Movies.Include(x=> x.Actors).Include(x=> x.Director).SingleOrDefault(m => m.Id == MovieId );
            if (movie is null)
            {
                throw new InvalidOperationException("Movie with this Id doesn't exists.");
            }
            var newMovie = _mapper.Map<Movie>(UpdateModel);
            if (UpdateModel.ActorIds.Count != 0)
            {
                movie.Actors.Clear();
               foreach (int actorId in UpdateModel.ActorIds)
                {
                    var actor = _dbContext.Actors.SingleOrDefault(m => m.Id == actorId);
                    if (actor is not null)
                    {
                        movie.Actors.Add(actor);
                    }
                    else
                    {
                        throw new InvalidOperationException("Actor with given Id " + actorId + "does not exists.");
                    }
                }
            }
            movie.Name = newMovie.Name == String.Empty ? movie.Name : newMovie.Name;
            movie.Date = newMovie.Date == default ? movie.Date : newMovie.Date;
            movie.Genre = newMovie.Genre == default ? movie.Genre : newMovie.Genre;
            movie.DirectorId  = newMovie.DirectorId == default ? movie.DirectorId : newMovie.DirectorId;
            movie.Price = newMovie.Price == default ? movie.Price : newMovie.Price;
            _dbContext.SaveChanges();
        }
    }

    public  class UpdateMovieModel
    {
        public string Name { get; set; } = String.Empty;
        public string Date { get; set; }  = String.Empty;
        public string Genre { get; set; }  = String.Empty;
        public int DirectorId  { get; set; } = 0;
        public  List<int> ActorIds { get; set; }  = new List<int>();
        public string Price { get; set; }  = 0.ToString();
    }
}