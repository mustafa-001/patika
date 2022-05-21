using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;

namespace WebApi.FligthOperations
{
    public class GetFligthsQuery
    {
        private readonly IFligthManagementDbContext _dbContext;

        public GetFligthsQuery(IFligthManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        private readonly IMapper _mapper;

        public List<FligthViewModel> Handle()
        {
            return _dbContext.Fligths.Include(x=> x.Pilots).Include(x=> x.Company).Select(f => _mapper.Map<FligthViewModel>(f)).ToList();
        }
    }

    public class FligthViewModel
    {
        public int Id { get; set; }
        public int PlaneId {get; set;}
        public string Company {get; set;} = null!;
        public List<int> PilotIds {get; set;} = new List<int>();
        public string DepartureAirfield {get; set;} = null!;
        public string ArrivalAirfield {get; set;} = null!;
        public DateTime DepartureTime {get; set;} 
        public DateTime ArrivalTime {get; set;}
    }
}