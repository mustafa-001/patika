using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.MovieOperations.GetMovieDetail
{
    public class GetMovieDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int MovieId;
        public GetMovieDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper, int movieId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            MovieId = movieId;
        }

        public MovieDetailModel Handle()
        {
            var movie = _dbContext.Movies.Include(x=> x.Director).Include(x=> x.Actors).SingleOrDefault(x => x.Id == MovieId);
            if (movie is null)
            throw new InvalidOperationException("Doesn't exists.");
            return _mapper.Map<MovieDetailModel>(movie);
        }
    }

    public class MovieDetailModel
    {
        public string Name{get; set;} = String.Empty;
        public string? Genre {get; set;}
        public int DirectorID {get; set;}
        public List<int> ActorIDs {get; set;} = new List<int>();
        public string? Date {get; set;}
    }
}