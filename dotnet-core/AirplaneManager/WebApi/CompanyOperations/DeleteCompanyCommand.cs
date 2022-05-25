using AutoMapper;
using WebApi.Database;

namespace WebApi.CompanyOperations
{
    public class DeleteCompanyCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly string _CompanyId;

        public DeleteCompanyCommand(IFligthManagementDbContext dbContext, string CompanyId)
        {
            _dbContext = dbContext;
            _CompanyId = CompanyId;
        }

        public void Handle()
        {
            var Company = _dbContext.Companies
            .SingleOrDefault(x=> x.Name == _CompanyId);
            if (Company is null)
            {
                throw new InvalidOperationException();
            }
            _dbContext.Companies.Remove(Company);
            _dbContext.SaveChanges();
        }

        
    }
    
}