using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;

namespace WebApi.CompanyOperations
{
    public class GetCompaniesQuery
    {
        private readonly IFligthManagementDbContext _dbContext;

        public GetCompaniesQuery(IFligthManagementDbContext dbContext )
        {
            _dbContext = dbContext;
        }


        public List<CompanyViewModel> Handle()
        {
            var Companys =  _dbContext.Companies.Select(x=> new CompanyViewModel{Name = x.Name})
            .ToList();
            return Companys;
        }
    }

    public class CompanyViewModel
    {
        public string Name { get; set; } = String.Empty;
    }

}