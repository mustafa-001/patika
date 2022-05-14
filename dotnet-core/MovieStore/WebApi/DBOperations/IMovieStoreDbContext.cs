using Microsoft.EntityFrameworkCore;

using WebApi.Entities;

namespace WebApi.DBOperations
{
    public interface IMovieStoreDbContext
    {
        DbSet<Movie> Movies{get; set;}
        DbSet<Director> Directors {get; set;}
        DbSet<Actor> Actors {get; set;}
        DbSet<User> Users {get; set;}
        int SaveChanges();
    }

}