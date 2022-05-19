using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.ActorOperations.GetActorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.ActorOperations.Commands.GetActorDetail
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

            var command = new GetActorDetailQuery(_context, _mapper, int.MaxValue);

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldGetAViewModel()
        {
            var actor = _context.Actors.SingleOrDefault(x=> x.Name == "Clint");
            var query = new GetActorDetailQuery(_context, _mapper, actor.Id);

            FluentActions.Invoking(() => query.Handle()).Invoke();
            actor = _context.Actors.SingleOrDefault(b => b.Id == actor.Id);

            actor.Name.Should().Be("Clint");
        }

    }
}