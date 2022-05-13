using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.GetBookById;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.GetBookDetail
{
    [Collection("NonParallelTestCollection")]
    public class GetBookDetailCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistingIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {

            var command = new GetBookByIdQuery(_context, _mapper);
            command.BookId = int.MaxValue;

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldGetAViewModel()
        {
            var command = new GetBookByIdQuery(_context, _mapper);
            var book = _context.Books.SingleOrDefault(x=> x.Title == "Dune");
            command.BookId = book.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();
            book = _context.Books.SingleOrDefault(b => b.Id == book.Id);

            book.Title.Should().Be("Dune");
        }

    }
}