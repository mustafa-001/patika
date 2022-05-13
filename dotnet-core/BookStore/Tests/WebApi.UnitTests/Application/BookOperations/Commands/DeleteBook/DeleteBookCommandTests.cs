using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    [Collection("NonParallelTestCollection")]
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistingIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {

            var command = new DeleteBookCommand(_context);
            command.BookId = int.MaxValue;

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldBeDeleted()
        {
            var command = new DeleteBookCommand(_context);
            var book = _context.Books.SingleOrDefault(x=> x.Title == "Dune");
            command.BookId = book.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();
            book = _context.Books.SingleOrDefault(b => b.Title == "Dune");

            book.Should().BeNull();
        }

        [Fact]
        public void WhenValidInputIsGiven_NoOtherBook_ShouldBeDeleted()
        {
            var command = new DeleteBookCommand(_context);
            var count = _context.Books.Count();
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();
            _context.Books.Count().Should().Be(count-1);

        }
    }
}