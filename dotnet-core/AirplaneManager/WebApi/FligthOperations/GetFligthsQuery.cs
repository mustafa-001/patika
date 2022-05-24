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

        public List<FligthDetailsViewModel> Handle()
        {
            var fligths =  _dbContext.Fligths
             .Include(x=> x.Pilots)
             .Include(x=> x.Plane)
             .Include(x=> x.Company)
             .Include(x=> x.DepartureAirfield).Include(x=> x.ArrivalAirfield).OrderBy(x=> x.Id)
            .ToList();
            return  _mapper.Map<List<FligthDetailsViewModel>>(fligths);
        }
    }

    public class FligthDetailsViewModel: FligthViewModel
    {
        public int Id { get; set; }
    }

    public class FligthViewModel
    {
        public int PlaneId {get; set;}
        public string Company {get; set;} = String.Empty;
        public List<int> PilotIds {get; set;} = new List<int>();
        public string DepartureAirfield {get; set;} = String.Empty!;
        public string ArrivalAirfield {get; set;} = string.Empty;
        public DateTime DepartureTime {get; set;} 
        public DateTime ArrivalTime {get; set;}
    }
}