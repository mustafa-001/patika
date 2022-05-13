using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{

    [Collection("NonParallelTestCollection")]
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesNotExistBookTitleIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {
            var book = new Book()
            {
                Title = "NotExistingTitle",
                PageCount = 200,
                PublishDate = DateTime.Now.AddYears(-1),
            };

            var command = new UpdateBookCommand(_context, _mapper);
            command.Model = new UpdateBookModel() { Title = book.Title };

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldBeUpdated()
        {
            var command = new UpdateBookCommand(_context, _mapper);
            var model = new UpdateBookModel() { Title = "LOTR", PageCount = 500, PublishDate = DateTime.Now.Date.AddYears(-1), GenreId = 1 };

            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();
            var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);

            book.Should().NotBeNull();
            if (book is not null)
            {
                book.PageCount.Should().Be(model.PageCount);
                book.PublishDate.Should().Be(model.PublishDate);
                book.GenreId.Should().Be(model.GenreId);
            }

        }
    }
}