using AutoMapper;
using WebApi.Database;

namespace WebApi.PlaneOperations
{
    public class DeletePlaneCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly int _PlaneId;

        public DeletePlaneCommand(IFligthManagementDbContext dbContext, int PlaneId)
        {
            _dbContext = dbContext;
            _PlaneId = PlaneId;
        }

        public void Handle()
        {
            var Plane = _dbContext.Planes
            .SingleOrDefault(x=> x.Id == _PlaneId);
            if (Plane is null)
            {
                throw new InvalidOperationException();
            }
            _dbContext.Planes.Remove(Plane);
            _dbContext.SaveChanges();
        }

        
    }
    
}