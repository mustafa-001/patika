
using System;
using System.Collections.Generic;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Directors
    {
        public static void AddDirectors(this IMovieStoreDbContext dbContext)
        {
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
                    },
                    new Director
                    {
                        Name = "Stanley",
                        Surname = "Kubrick",
                        Movies = new List<Movie>()
                    }
                );
    }
}
}

