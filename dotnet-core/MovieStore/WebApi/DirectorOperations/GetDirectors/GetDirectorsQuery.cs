using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DirectorOperations.GetDirectors
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetDirectorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<DirectorsViewModel> Handle()
        {
            var DirectorList = _dbContext.Directors.Include(x=> x.Movies).OrderBy(x => x.Id).ToList();
            return _mapper.Map<List<DirectorsViewModel>>(DirectorList);
        }
    }

    public class DirectorsViewModel
    {
        public string Name{get; set;} = String.Empty;
        public string? Surname {get; set;}
        public string? Genre {get; set;}
        public List<int> MovieIDs {get; set;} = new List<int>();
        public string? BirthDate {get; set;}
    }
}