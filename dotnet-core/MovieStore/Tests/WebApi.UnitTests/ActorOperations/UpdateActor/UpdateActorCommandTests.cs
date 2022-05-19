using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.ActorOperations.UpdateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using System.Collections.Generic;

namespace Application.ActorOperations.Commands.UpdateActor
{

    [Collection("NonParallelTestCollection")]
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesNotExistActorIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {
            var model = new UpdateActorModel()
            {
                Name = "Eli",
                Surname = "Wallach",
                BirthDate = "1950, 12, 12",
                MovieIDs = new List<int>(),
            };
            var command = new UpdateActorCommand(_context, _mapper, model, int.MaxValue);

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeUpdated()
        {
            var model = new UpdateActorModel()
            {
                Name = "Eli",
                Surname = "Wallach",
                BirthDate = "12/12/1950",
                MovieIDs = new List<int>(),
            };
            var command = new UpdateActorCommand(_context, _mapper, model, 1);


            FluentActions.Invoking(() => command.Handle()).Invoke();
            var actor = _context.Actors.SingleOrDefault(b => b.Id == 1);

            actor.Should().NotBeNull();
            if (actor is not null)
            {
                actor.Name.ToString().Should().Be(model.Name);
                actor.BirthDate.ToShortDateString().Should().Be(model.BirthDate);
            }

        }
       [Fact]
        public void WhenAMovieAddedToActor_NewRelationship_ShouldBeEstablishedAtBothEnds(){
            var actor = _context.Actors.First(x=> x.Name == "Elijah");
            var oldMovie = actor.Movies.Single(x => x.Name == "The Lord of the Rings");
            var movie = _context.Movies.Single(m => m.Name == "The Good, the Bad and the Ugly" );
            var actorModel = new UpdateActorModel{
                MovieIDs = new List<int>{movie.Id}
            };

            var command = new UpdateActorCommand(_context, _mapper, actorModel, actor.Id);
            command.Handle();
            movie.Actors.Should().Contain(actor);
            actor.Movies.Select(x => x.Name).Should().Contain("The Good, the Bad and the Ugly");
            actor.Movies.Count.Should().Be(1);
        }

        [Fact]
        public void WhenAMovieAddedToActor_OldRelationShip_ShouldBeREmovedAtBothEnds(){
            var actor = _context.Actors.First(x => x.Name == "Ian");
            var oldMovie = actor.Movies.First(x => true);
            var movie = _context.Movies.First(m => m.Name != oldMovie.Name);
            var oldMovieActorCount = oldMovie.Actors.Count;

            var actorModel = new UpdateActorModel{
                MovieIDs = new List<int>{movie.Id}
            };

            var command = new UpdateActorCommand(_context, _mapper, actorModel, actor.Id);
            command.Handle();

            actor.Movies.Select(x => x.Name).Should().Contain("The Good, the Bad and the Ugly");
            movie.Actors.Should().Contain(actor);
            oldMovie.Actors.Select(x=> x.Name).Should().NotContain("Ian");
            oldMovie.Actors.Count.Should().Be(oldMovieActorCount -1);
        }
    }
}