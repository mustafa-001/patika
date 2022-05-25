using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.FligthOperations
{
    public class UpdateFligthCommand
    {
        private readonly IFligthManagementDbContext _dbContext;

        private readonly IMapper _mapper;
        private readonly FligthViewModel _model;
        private readonly int _fligthId;

        public UpdateFligthCommand(IFligthManagementDbContext dbContext, IMapper mapper, FligthViewModel model, int fligthId)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _model = model;
            _fligthId = fligthId;
        }
        public void Handle()
        {
            var fligth = _dbContext.Fligths.Include(x=>x.Pilots)
            .SingleOrDefault(x=> x.Id == _fligthId);
            if (fligth is null)
            {
                throw new InvalidOperationException();
            }


            fligth.ArrivalTime = _model.ArrivalTime == default  ? fligth.ArrivalTime : _model.ArrivalTime;
            fligth.DepartureTime = _model.DepartureTime == default  ? fligth.DepartureTime : _model.DepartureTime;
            //`default` of Non-null reference type is still null, it it doesn't even throw NPE.
            fligth.ArrivalAirfield = _model.ArrivalAirfield == String.Empty ? fligth.ArrivalAirfield :    _dbContext.Airfields.Find(_model.ArrivalAirfield)!;
            fligth.DepartureAirfield = _model.DepartureAirfield == String.Empty ? fligth.DepartureAirfield : _dbContext.Airfields.Find(_model.DepartureAirfield)!;
            fligth.Plane = _model.PlaneId == default ? fligth.Plane : _dbContext.Planes.Find(_model.PlaneId)!;
            fligth.Company = _model.Company == String.Empty ? fligth.Company : _dbContext.Companies.Find(_model.Company)!;

            if ( _model.PilotIds.Count is not 0){
                fligth.Pilots.Clear();
                foreach (var p in _model.PilotIds)
                {
                    fligth.Pilots.Add(_dbContext.Pilots.Single(x=> x.Id == p));
                }
            }
            _dbContext.SaveChanges();
        }
    }

}