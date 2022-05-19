using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DirectorOperations.GetDirectorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.DirectorOperations.Commands.GetDirectorDetail
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

            var command = new GetDirectorDetailQuery(_context, _mapper, int.MaxValue);

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldGetAViewModel()
        {
            var Director = _context.Directors.SingleOrDefault(x=> x.Name == "Sergio");
            var query = new GetDirectorDetailQuery(_context, _mapper, Director.Id);

            FluentActions.Invoking(() => query.Handle()).Invoke();
            Director = _context.Directors.SingleOrDefault(b => b.Id == Director.Id);

            Director.Name.Should().Be("Sergio");
        }

    }
}