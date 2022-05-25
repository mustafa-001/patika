using AutoMapper;
using WebApi.Database;

namespace WebApi.AirfieldOperations
{
    public class DeleteAirfieldCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly string _AirfieldId;

        public DeleteAirfieldCommand(IFligthManagementDbContext dbContext, string AirfieldId)
        {
            _dbContext = dbContext;
            _AirfieldId = AirfieldId;
        }

        public void Handle()
        {
            var Airfield = _dbContext.Airfields
            .SingleOrDefault(x=> x.Id == _AirfieldId);
            if (Airfield is null)
            {
                throw new InvalidOperationException();
            }
            _dbContext.Airfields.Remove(Airfield);
            _dbContext.SaveChanges();
        }

        
    }
    
}