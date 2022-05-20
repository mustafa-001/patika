using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ActorOperations.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ActorsViewModel> Handle()
        {
            var ActorList = _dbContext.Actors.Include(x=> x.Movies).OrderBy(x => x.Id).ToList();
            return _mapper.Map<List<ActorsViewModel>>(ActorList);
        }
    }

    public class ActorsViewModel
    {
        public string Name{get; set;} = String.Empty;
        public string? Genre {get; set;}
        public List<int> MovieIDs {get; set;} = new List<int>();
        public string? BirthDate {get; set;}
    }
}