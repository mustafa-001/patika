using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DirectorOperations.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private UpdateDirectorModel UpdateModel;
        public int DirectorId;

        private void LinkDirectorToMovies(IMovieStoreDbContext dbContext, List<int> movieIds)
        {

        }


        public UpdateDirectorCommand(IMovieStoreDbContext dbContext, IMapper mapper, UpdateDirectorModel updateModel, int directorId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            UpdateModel = updateModel;
            DirectorId = directorId;
        }
        public void Handle()
        {
            var director = _dbContext.Directors.Include(x => x.Movies).SingleOrDefault(m => m.Id == DirectorId);
            if (director is null)
            {
                throw new InvalidOperationException("This Director doesn't exist.");
            }
            var newDirector = _mapper.Map<Director>(UpdateModel);

            if (UpdateModel.MovieIDs.Count != 0)
            {
                // foreach(var movie in _dbContext.Movies.Where(x => x.Director == director).AsEnumerable())
                // {
                //     movie.DirectorId = null;
                // }
                director.Movies.Clear();
               foreach (int movieId in UpdateModel.MovieIDs)
                {
                    var m = _dbContext.Movies.SingleOrDefault(m => m.Id == movieId);
                    if (m is not null)
                    {
                        director.Movies.Add(m);
                    }
                    else
                    {
                        throw new InvalidOperationException("Movie with given Id " + movieId + "does not exists.");
                    }
                }
            }
            director.Name = newDirector.Name == default ? director.Name : newDirector.Name;
            director.Surname = newDirector.Surname == default ? director.Surname : newDirector.Surname;
            director.BirthDate = newDirector.BirthDate == default ? director.BirthDate : newDirector.BirthDate;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateDirectorModel
    {
        public string Name = String.Empty;
        public string Surname = String.Empty;
        public List<int> MovieIDs = new List<int>();
        public string BirthDate = String.Empty;
    }
}