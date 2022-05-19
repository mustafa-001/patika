using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DirectorOperations.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int DirectorId;
        public DeleteDirectorCommand(IMovieStoreDbContext dbContext, int directorId)
        {
            _dbContext = dbContext;
            DirectorId = directorId;
        }

        public void Handle()
        {
            var Director = _dbContext.Directors.SingleOrDefault(x => x.Id == DirectorId);
            if (_dbContext.Movies.FirstOrDefault(m => m.Director == Director) is not null)
            {
                throw new InvalidOperationException("Director is referenced in a movie entity.");
            }
            if (Director is null)
            throw new InvalidOperationException("Doesn't exists.");

            _dbContext.Directors.Remove(Director);
            _dbContext.SaveChanges();
        }
    }
}

