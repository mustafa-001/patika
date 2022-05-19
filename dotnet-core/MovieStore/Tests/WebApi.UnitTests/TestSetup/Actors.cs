using System;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Actors
    {
        public static void AddActors(this IMovieStoreDbContext dbContext)
        {
     
            dbContext.Actors.AddRange(
                new Actor
                {
                    Name = "Elijah",
                    Surname = "Wood",
                    Movies = new List<Movie>()
                },
                new Actor
                {
                    Name = "Ian",
                    Surname = "McKellen",
                    Movies = new List<Movie>()
                },
                new Actor
                {
                    Name = "Lee",
                    Surname = "Van Cleef",
                    Movies = new List<Movie>()
                },
                new Actor
                {
                    Name = "Clint",
                    Surname = "Eastwood",
                    Movies = new List<Movie>()
                }
            );
        }
    }
}