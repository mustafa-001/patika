using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.MovieOperations.DeleteMovie;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.MovieOperations.Commands.DeleteMovie
{
    [Collection("NonParallelTestCollection")]
    public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistingIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {

            var command = new DeleteMovieCommand(_context, Int16.MaxValue);

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Movie_ShouldBeDeleted()
        {
            var movie = _context.Movies.SingleOrDefault(x=> x.Name == "The Lord of the Rings");
            var command = new DeleteMovieCommand(_context, movie.Id);

            FluentActions.Invoking(() => command.Handle()).Invoke();
            movie = _context.Movies.SingleOrDefault(b => b.Name == "The Lord of the Rings");

            movie.Should().BeNull();
        }

        [Fact]
        public void WhenValidInputIsGiven_NoOtherMovie_ShouldBeDeleted()
        {
            var command = new DeleteMovieCommand(_context, 2);
            var count = _context.Movies.Count();

            FluentActions.Invoking(() => command.Handle()).Invoke();
            _context.Movies.Count().Should().Be(count-1);

        }
    }
}