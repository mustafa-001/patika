using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {
            var book = new Book()
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationsExceptions_ShouldBeThrown",
                PageCount = 200,
                PublishDate = DateTime.Now.AddYears(-1),
            };
            _context.SaveChanges();
            _context.Books.Add(book);
            _context.SaveChanges();

            var command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };

            FluentActions
            .Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldBeCreated()
        {
            var command = new CreateBookCommand(_context, _mapper);
            var model = new CreateBookModel() { Title = "Hobbit", PageCount = 500, PublishDate = DateTime.Now.Date.AddYears(-1), GenreId = 1 };

            command.Model = model;
            _context.SaveChanges();

            FluentActions.Invoking(() => command.Handle()).Invoke();
            var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);

        }
    }
}