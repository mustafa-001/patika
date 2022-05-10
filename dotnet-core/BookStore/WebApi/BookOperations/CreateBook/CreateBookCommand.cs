
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using AutoMapper;
using WebApi.Common;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand 
    {
        public CreateBookModel Model {get; set;}
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            if (_dbContext.Books.SingleOrDefault(x=> x.Title == Model.Title) is not null)
            throw new InvalidOperationException("Book Already Exist.");

            var book = _mapper.Map<Book>(Model);
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }

        public class CreateBookModel
        {
            public string Title {get; set;}
            public int GenreId {get; set;}
            public int PageCount {get; set;}
            public DateTime PublishDate {get; set;}
    
        }
}