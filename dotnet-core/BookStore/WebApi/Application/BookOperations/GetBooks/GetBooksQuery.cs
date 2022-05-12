using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(x=> x.Genre).Include(x=> x.Author).OrderBy(x => x.Id).ToList();
            return _mapper.Map<List<BooksViewModel>>(bookList);
        }
    }

    public class BooksViewModel
    {
        public string Title{get; set;}
        public int PageCount {get; set;}
        public string Genre {get; set;}
        public string Author {get; set;}
        public string  PublishDate {get; set;}
    }
}