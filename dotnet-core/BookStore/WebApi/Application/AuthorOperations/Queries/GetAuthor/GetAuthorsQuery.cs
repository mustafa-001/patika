using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var Authors = _context.Authors.OrderBy(x=>x.Id).ToList();
            return  _mapper.Map<List<AuthorsViewModel>>(Authors);
        }
    }

    public class AuthorsViewModel
    {
        public int Id {get;set  ;}
        public string Name {get; set;}
        public string BirthDate {get; set;}
    }
}