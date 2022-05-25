using AutoMapper;
using WebApi.Database;

namespace WebApi.PlaneModelOperations
{
    public class DeletePlaneModelCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly int _PlaneModelId;

        public DeletePlaneModelCommand(IFligthManagementDbContext dbContext, int PlaneModelId)
        {
            _dbContext = dbContext;
            _PlaneModelId = PlaneModelId;
        }

        public void Handle()
        {
            var PlaneModel = _dbContext.PlaneModels
            .SingleOrDefault(x=> x.Id == _PlaneModelId);
            if (PlaneModel is null)
            {
                throw new InvalidOperationException();
            }
            _dbContext.PlaneModels.Remove(PlaneModel);
            _dbContext.SaveChanges();
        }

        
    }
    
}