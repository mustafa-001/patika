
using System;
using System.Linq;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Entities;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is null)
                throw new InvalidOperationException("Book Does Not Exist.");

            var newBook = _mapper.Map<Book>(Model);
            book.PageCount = newBook.PageCount == default ? book.PageCount : newBook.PageCount;
            book.PublishDate = newBook.PublishDate == default ? book.PublishDate : newBook.PublishDate;
            book.GenreId = newBook.GenreId == default ? book.GenreId : newBook.GenreId;
            book.AuthorId = newBook.AuthorId == default ? book.AuthorId : newBook.AuthorId;

            _dbContext.SaveChanges();
        }
    }


    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}