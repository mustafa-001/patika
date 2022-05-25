using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;

namespace WebApi.PilotOperations
{
    public class GetPilotsQuery
    {
        private readonly IFligthManagementDbContext _dbContext;

        public GetPilotsQuery(IFligthManagementDbContext dbContext )
        {
            _dbContext = dbContext;
        }


        public List<PilotViewModel> Handle()
        {
            var pilots =  _dbContext.Pilots
            .Include(x=> x.Fligths)
            .Select(x=> new PilotViewModel{Id = x.Id, FligthIds = x.Fligths.Select(f=> f.Id).ToList()})
            .ToList();
            return pilots;
        }
    }

    public class PilotViewModel
    {
        public int Id { get; set; }
        public List<int> FligthIds {get; set;} = new List<int>();
    }

}