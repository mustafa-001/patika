using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.PilotOperations
{
    public class CreatePilotCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PilotViewModel _model;

        public CreatePilotCommand(IFligthManagementDbContext dbContext, IMapper mapper,   PilotViewModel model)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _model = model;
        }


        public void Handle()
        {
            _dbContext.Pilots.Add(new Pilot());
            _dbContext.SaveChanges();
        }
    }

}