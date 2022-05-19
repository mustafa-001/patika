using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.ActorOperations.DeleteActor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.ActorOperations.Commands.DeleteActor
{
    [Collection("NonParallelTestCollection")]
    public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistingIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {

            var command = new DeleteActorCommand(_context, Int16.MaxValue);

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void WhenAMovieReferencesThisActor_InvalidOperationsException_ShouldBeThrown()
        {

            var actor = _context.Actors.Single(x=> x.Name == "Elijah");
            var command = new DeleteActorCommand(_context, actor.Id);

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();

        }

        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeDeleted()
        {
            var actor = new Actor {
                Name = "Sth",
                Surname = "sth",
                BirthDate = default,
                Movies = default
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            var command = new DeleteActorCommand(_context, actor.Id);

            FluentActions.Invoking(() => command.Handle()).Invoke();
            actor = _context.Actors.SingleOrDefault(b => b.Name == "Sth" );

            actor.Should().BeNull();
        }

        [Fact]
        public void WhenValidInputIsGiven_NoOtherActor_ShouldBeDeleted()
        {
            var actor = new Actor {
                Name = "Sth",
                Surname = "sth",
                BirthDate = default,
                Movies = default
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();
            var count = _context.Actors.Count();

            var command = new DeleteActorCommand(_context, actor.Id);

            FluentActions.Invoking(() => command.Handle()).Invoke();
            _context.Actors.Count().Should().Be(count-1);

        }
    }
}