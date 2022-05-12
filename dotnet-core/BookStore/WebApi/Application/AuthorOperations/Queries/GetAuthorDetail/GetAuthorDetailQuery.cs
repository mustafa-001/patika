using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId {get; set;}
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var Author = _context.Authors.SingleOrDefault(x => (x.Id == AuthorId));
            if (Author is null)
            throw  new Exception("Author does not exist");
            return _mapper.Map<AuthorDetailViewModel>(Author);
        }
    }

    public class AuthorDetailViewModel
    {
        public int Id {get;set;}
        public string Name {get; set;}
        public string BirthDate {get; set;}
    }
}