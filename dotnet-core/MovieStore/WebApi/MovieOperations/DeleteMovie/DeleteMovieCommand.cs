using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.MovieOperations.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int MovieId;
        public DeleteMovieCommand(IMovieStoreDbContext dbContext, int movieId)
        {
            _dbContext = dbContext;
            MovieId = movieId;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.SingleOrDefault(x => x.Id == MovieId);
            if (movie is null)
            throw new InvalidOperationException("Doesn't exists.");
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
        }
    }
}

