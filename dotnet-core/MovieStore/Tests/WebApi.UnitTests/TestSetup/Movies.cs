using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestSetup
{
    public static class Movies
    {
        public static void AddMovies(this IMovieStoreDbContext dbContext)
        {
            if (dbContext.Movies.Any())
                {
                    return;
                };

                dbContext.Movies.AddRange(
                    new Movie
                    {
                        Name = "The Lord of the Rings",
                        Genre = "Fantasy",
                        Actors = new List<Actor>()
                        {
                            dbContext.Actors.First(a => a.Name == "Elijah"),
                            dbContext.Actors.First(a => a.Name == "Ian")
                        },
                        Director = dbContext.Directors.Single(d => d.Name == "Peter"),
                        Price = 5,
                        Date = new DateTime(2001 ,08, 01)
                    },

                    new Movie
                    {
                        Name = "The Good, the Bad and the Ugly",
                        Genre = "Western", //Science Fiction
                        Actors = new List<Actor>()
                        {
                            dbContext.Actors.FirstOrDefault(a => a.Name == "Clint"),
                            dbContext.Actors.SingleOrDefault(a => a.Name == "Lee")
                        },
                        Director = dbContext.Directors.Single(d => d.Name == "Sergio"),
                        Price = 5,
                        Date = new DateTime(1966, 12, 08)
                    },
                    new Movie
                    {
                        Name = "Dr Strangelove",
                        Genre = "Science Fiction", 
                        Actors = new List<Actor>()
                        {},
                        Director = dbContext.Directors.Single(d => d.Name == "Stanley"),
                        Price = 5,
                        Date = new DateTime(1950, 12, 08)
                    }
                );

        }
    }
}