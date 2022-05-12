using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly int _id;
        public int BookId;
        public GetBookByIdQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public GetBookByIdModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Include(x=> x.Author).SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Book Does Not Exist");
            return _mapper.Map<GetBookByIdModel>(book);
        }
    }


    public class GetBookByIdModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author {get; set;}
        public int PageCount { get; set; }
        public string PublishDate { get; set; }

    }
}