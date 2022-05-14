using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new MovieStoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {

                if (dbContext.Movies.Any())
                {
                    return;
                }

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

                dbContext.Directors.AddRange(
                    new Director
                    {
                        Name = "Sergio",
                        Surname = "Leone",
                        Movies = new List<Movie>()
                    },
                    new Director
                    {
                        Name = "Peter",
                        Surname = "Jackson",
                        Movies = new List<Movie>()
                    }
                );

                dbContext.SaveChanges();
                dbContext.Movies.AddRange(
                    new Movie
                    {
                        Name = "Lord Of The Rings",
                        Genre = "Fantasy",
                        Actors = new List<Actor>()
                        {
                            dbContext.Actors.FirstOrDefault(a => a.Name == "Elijah"),
                            dbContext.Actors.FirstOrDefault(a => a.Name == "Ian")
                        },
                        Director = dbContext.Directors.FirstOrDefault(d => d.Name == "Peter"),
                        Price = 5,
                        Year = new DateOnly(2001 ,08, 01)
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
                        Director = dbContext.Directors.SingleOrDefault(d => d.Name == "Sergio"),
                        Price = 5,
                        Year = new DateOnly(1966, 12, 08)
                    }
                );
                dbContext.SaveChanges();
            }
        }
    }
}