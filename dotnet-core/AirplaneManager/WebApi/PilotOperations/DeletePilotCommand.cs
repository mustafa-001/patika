using AutoMapper;
using WebApi.Database;

namespace WebApi.PilotOperations
{
    public class DeletePilotCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly int _pilotId;

        public DeletePilotCommand(IFligthManagementDbContext dbContext, int pilotId)
        {
            _dbContext = dbContext;
            _pilotId = pilotId;
        }

        public void Handle()
        {
            var Pilot = _dbContext.Pilots
            .SingleOrDefault(x=> x.Id == _pilotId);
            if (Pilot is null)
            {
                throw new InvalidOperationException();
            }
            if (Pilot.Fligths.Count is not 0)
            {
                throw new InvalidOperationException();
            }
            _dbContext.Pilots.Remove(Pilot);
            _dbContext.SaveChanges();
        }

        
    }
    
}