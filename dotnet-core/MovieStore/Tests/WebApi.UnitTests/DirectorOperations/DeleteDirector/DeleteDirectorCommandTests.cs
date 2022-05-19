using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DirectorOperations.DeleteDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.DirectorOperations.Commands.DeleteDirector
{
    [Collection("NonParallelTestCollection")]
    public class DeleteDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistingIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {

            var command = new DeleteDirectorCommand(_context, Int16.MaxValue);

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void WhenAMovieReferencesThisDirector_InvalidOperationsException_ShouldBeThrown()
        {

            var director = _context.Directors.Single(x=> x.Name == "Sergio");
            var command = new DeleteDirectorCommand(_context, director.Id);

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();

        }


        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeDeleted()
        {
            var Director = new Director {
                Name = "Sth",
                Surname = "sth",
                BirthDate = default,
                Movies = default
            };
            _context.Directors.Add(Director);
            _context.SaveChanges();

            var command = new DeleteDirectorCommand(_context, Director.Id);

            FluentActions.Invoking(() => command.Handle()).Invoke();
            Director = _context.Directors.SingleOrDefault(b => b.Name == "Sth" );

            Director.Should().BeNull();
        }

        [Fact]
        public void WhenValidInputIsGiven_NoOtherDirector_ShouldBeDeleted()
        {
            var Director = new Director {
                Name = "Sth",
                Surname = "sth",
                BirthDate = default,
                Movies = default
            };
            _context.Directors.Add(Director);
            _context.SaveChanges();
            var count = _context.Directors.Count();

            var command = new DeleteDirectorCommand(_context, Director.Id);

            FluentActions.Invoking(() => command.Handle()).Invoke();
            _context.Directors.Count().Should().Be(count-1);

        }
    }
}