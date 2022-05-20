using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DirectorOperations.GetDirectorDetail
{
    public class GetDirectorDetailQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int DirectorId;
        public GetDirectorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper, int directorId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            DirectorId = directorId;
        }

        public DirectorDetailModel Handle()
        {
            var Director = _dbContext.Directors.Include(x=> x.Movies).SingleOrDefault(x => x.Id == DirectorId);
            if (Director is null)
            throw new InvalidOperationException("Doesn't exists.");
            return _mapper.Map<DirectorDetailModel>(Director);
        }
    }

    public class DirectorDetailModel
    {
        public string Name{get; set;} =  String.Empty;
        public string? Surnamee {get; set;}
        public List<int> MovieIDs {get; set;} = new List<int>();
        public string? BirthDate {get; set;}
    }
}