using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using Microsoft.EntityFrameworkCore;
using WebApi.MovieOperations.UpdateMovie;
using WebApi.DBOperations;
using Xunit;
using System.Collections.Generic;

namespace Application.MovieOperations.Commands.UpdateMovie
{

    [Collection("NonParallelTestCollection")]
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesNotExistMovieIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {
            var model = new UpdateMovieModel()
                    {
                        Name = "Lord Of The Rings",
                    };

            var command = new UpdateMovieCommand(_context, _mapper, model, int.MaxValue);

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Movie_ShouldBeUpdated()
        {
            var model = new UpdateMovieModel() {Name = "The Lord of The Rings", Genre = "Scifi", ActorIds = new List<int>(),
            DirectorId = default, Price = 10.ToString(), Date = DateTime.Now.Date.AddYears(-1).ToString() };
            var command = new UpdateMovieCommand(_context, _mapper, model, 1);


            FluentActions.Invoking(() => command.Handle()).Invoke();
            var movie = _context.Movies.SingleOrDefault(b => b.Id == 1);

            movie.Should().NotBeNull();
            if (movie is not null)
            {
                movie.Genre.ToString().Should().Be(model.Genre);
                movie.Date.ToString().Should().Be(model.Date);
            }
        }

        [Fact]
        public void WhenAnActorAddedToMovie_NewRelationship_ShouldBeEstablishedAtBothEnds(){
            var oldActor = _context.Actors.Include(x=> x.Movies).Single(x=> x.Name == "Elijah");
            var movie = oldActor.Movies.Single(x => x.Name == "The Lord of the Rings");
            var actor = _context.Actors.Single(m => m.Name == "Clint");
            var movieModel = new UpdateMovieModel{
                ActorIds = new List<int>{actor.Id}
            };

            var command = new UpdateMovieCommand(_context, _mapper, movieModel, movie.Id);
            command.Handle();
            movie.Actors.Should().Contain(actor);
            actor.Movies.Select(x => x.Name).Should().Contain("The Lord of the Rings");
            actor.Movies.Count().Should().Be(2);
        }

        [Fact]
        public void WhenAnActorAddedToMovie_OldRelationShip_ShouldBeREmovedAtBothEnds(){
            var oldActor = _context.Actors.Single(x=> x.Name == "Ian");
            var oldActorMovieCount = oldActor.Movies.Count;
            var movie = oldActor.Movies.Single(x => x.Name == "The Lord of the Rings");
            var actor = _context.Actors.Single(m => m.Name == "Lee");
            var movieModel = new UpdateMovieModel{
                ActorIds = new List<int>{actor.Id}
            };

            var command = new UpdateMovieCommand(_context, _mapper, movieModel, movie.Id);
            command.Handle();
            movie.Actors.Should().NotContain(oldActor);
            // actor.Movies.Select(x => x.Name).Should().NotContain("The Lord of the Rings");
            oldActor.Movies.Should().NotContain(movie);
            oldActor.Movies.Count.Should().Be(oldActorMovieCount-1);

            var elijah = _context.Actors.Single(x=>x.Name == "Elijah");
            movie.Actors.Add(elijah);
            _context.SaveChanges();
        }

        [Fact]
        public void WhenADirectorAddedToMovie_NewRelationship_ShouldBeEstablishedAtBothEnds(){
            var oldDirector = _context.Directors.First(x=> x.Name == "Stanley");
            var movie = oldDirector.Movies.Single(x => x.Name == "Dr Strangelove");
            var director = _context.Directors.Single(m => m.Name == "Sergio");
            var directorMovieCountBefore = director.Movies.Count;
            var movieModel = new UpdateMovieModel{
                DirectorId = director.Id
            };

            var command = new UpdateMovieCommand(_context, _mapper, movieModel, movie.Id);
            command.Handle();
            director.Movies.Should().Contain(movie);
            director.Movies.Select(x=> x.Name).Should().Contain("Dr Strangelove");
            director.Movies.Count.Should().Be(directorMovieCountBefore+1);
            movie.Director.Should().Be(director);
        }

        [Fact]
        public void WhenADirectorAddedToMovie_OldRelationShip_ShouldBeREmovedAtBothEnds(){
            var oldDirector = _context.Directors.First(x => x.Name == "Sergio");
            var oldDirectorMovieCount = oldDirector.Movies.Count;
            var director = _context.Directors.Single(x=> x.Name =="Peter");
            var movie = _context.Movies.First(m => m.Name == "The Good, the Bad and the Ugly");

            var movieModel = new UpdateMovieModel{
                DirectorId = director.Id
            };

            var command = new UpdateMovieCommand(_context, _mapper, movieModel, movie.Id);
            command.Handle();

            oldDirector.Movies.Select(x => x.Name).Should().NotContain("The Good, the Bad and the Ugly");
            oldDirector.Movies.Count.Should().Be(oldDirectorMovieCount-1);
        }
    }
}