using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ActorOperations.DeleteActor
{
    public class DeleteActorCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        public int ActorId;
        public DeleteActorCommand(IMovieStoreDbContext dbContext, int actorId)
        {
            _dbContext = dbContext;
            ActorId = actorId;
        }

        public void Handle()
        {
            var actor = _dbContext.Actors.SingleOrDefault(x => x.Id == ActorId);
            if (_dbContext.Movies.FirstOrDefault(m => m.Actors.FirstOrDefault(a => a == actor) != null) is not null)
            {
                throw new InvalidOperationException("Actor is referenced in a movie entity.");
            }
            if (actor is null)
            throw new InvalidOperationException("Doesn't exists.");

            _dbContext.Actors.Remove(actor);
            _dbContext.SaveChanges();
        }
    }
}

