using AutoMapper;
using WebApi.Database;

namespace WebApi.FligthOperations
{
    public class DeleteFligthCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly int _fligthId;
        public DeleteFligthCommand(IFligthManagementDbContext dbContext,  int fligthId)
        {
            _dbContext = dbContext;
            _fligthId = fligthId;
        }
        public void Handle()
        {
            var fligth = _dbContext.Fligths
            .SingleOrDefault(x=> x.Id == _fligthId);
            if (fligth is null)
            {
                throw new InvalidOperationException();
            }
            if (fligth.Pilots.Count is not 0)
            {
                throw new InvalidOperationException();
            }
            _dbContext.Fligths.Remove(fligth);
            _dbContext.SaveChanges();
        }

        
    }
    
}