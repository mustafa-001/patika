using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres = _context.Genres.OrderBy(x=>x.Id).ToList();
            return  _mapper.Map<List<GenresViewModel>>(genres);
        }
    }

    public class GenresViewModel
    {
        public int Id {get;set;}
        public string Name {get; set;}
        public bool IsActive {get; set;}
    }
}