using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.MovieOperations.GetMovieDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.MovieOperations.Commands.GetMovieDetail
{
    [Collection("NonParallelTestCollection")]
    public class GetBookDetailCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistingIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {

            var command = new GetMovieDetailQuery(_context, _mapper, int.MaxValue);

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldGetAViewModel()
        {
            var movie = _context.Movies.SingleOrDefault(x=> x.Name == "The Lord of the Rings");
            var query = new GetMovieDetailQuery(_context, _mapper, movie.Id);

            FluentActions.Invoking(() => query.Handle()).Invoke();
            movie = _context.Movies.SingleOrDefault(b => b.Id == movie.Id);

            movie.Name.Should().Be("The Lord of the Rings");
        }

    }
}