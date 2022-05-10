using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly int _id;
        public GetBookByIdQuery(BookStoreDbContext dbContext, int id)
        {
            _dbContext = dbContext;
            _id = id;
        }

        public GetBookByIdModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == _id);
            if (book is null)
                throw new InvalidOperationException("Book Does Not Exist");
            return new GetBookByIdModel()
            {
                Title = book.Title,
                Genre = ((Genre)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                PageCount = book.PageCount
            };
        }
    }


    public class GetBookByIdModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }
}