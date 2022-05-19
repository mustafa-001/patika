using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.MovieOperations.CreateMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using System.Collections.Generic;

namespace Application.MovieOperations.Commands.CreateMovie
{    
    [Collection("NonParallelTestCollection")]
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistMovieTitleIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {
            var movie = new Movie()
                    {
                        Name = "The Lord of the Rings",
                        Genre = "Fantasy",
                        Actors = new List<Actor>()
                        {
                            _dbContext.Actors.FirstOrDefault(a => a.Name == "Elijah"),
                            _dbContext.Actors.FirstOrDefault(a => a.Name == "Ian")
                        },
                        Director = _dbContext.Directors.FirstOrDefault(d => d.Name == "Peter"),
                        Price = 5,
                        Date = new DateTime(2001 ,08, 01)
                    };


            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            var model = new CreateMovieModel() { Name = movie.Name };
            var command = new CreateMovieCommand(_dbContext, _mapper, model);

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Movie_ShouldBeCreated()
        {
            var model = new CreateMovieModel() {
                Name = "Movie",
                Genre = "Horror",
                Actors = new List<int>(),
                DirectorId = 1,
                Price =  10.ToString(),
                Date = "12/10/1990"
            };

                
            var command = new CreateMovieCommand(_dbContext, _mapper, model);


            FluentActions.Invoking(() => command.Handle()).Invoke();
            var movie = _dbContext.Movies.SingleOrDefault(b => b.Name == model.Name);

            movie.Should().NotBeNull();
            if (movie is not null)
            {
                movie.Name.Should().Be(model.Name);
                movie.Genre.Should().Be(model.Genre);
                movie.Date.ToShortDateString().Should().Be(model.Date);
            }

        }
    }
}