using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ActorOperations.GetActorDetail
{
    public class GetActorDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int ActorId;
        public GetActorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper, int actorId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            ActorId = actorId;
        }

        public ActorDetailModel Handle()
        {
            var Actor = _dbContext.Actors.Include(x=> x.Movies).SingleOrDefault(x => x.Id == ActorId);
            if (Actor is null)
            throw new InvalidOperationException("Doesn't exists.");
            return _mapper.Map<ActorDetailModel>(Actor);
        }
    }

    public class ActorDetailModel
    {
        public string Name{get; set;} = String.Empty;
        public string? Surname{get; set;}
        public List<int> MovieIDs {get; set;} = new List<int>();
        public string? BirthDate {get; set;}
    }
}