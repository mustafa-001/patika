using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations 
{
    public class MovieStoreDbContext : DbContext, IMovieStoreDbContext
    {
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options): base(options){}
        public DbSet<Movie> Movies  {get; set;} 
        public DbSet<Director> Directors {get; set;}
        public DbSet<Actor> Actors{get; set;}
        public DbSet<User> Users {get; set;}

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }

}