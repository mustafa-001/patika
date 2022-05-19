using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.MovieOperations.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<MoviesViewModel> Handle()
        {
            var movieList = _dbContext.Movies.Include(x=> x.Director).OrderBy(x => x.Id).ToList();
            return _mapper.Map<List<MoviesViewModel>>(movieList);
        }
    }

    public class MoviesViewModel
    {
        public string Name{get; set;}
        public string Genre {get; set;}
        public int DirectorID {get; set;}
        public List<int> ActorIDs {get; set;}
        public string Date {get; set;}
    }
}