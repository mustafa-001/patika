using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.CompanyOperations
{
    public class CreateCompanyCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly CompanyViewModel _model;

        public CreateCompanyCommand(IFligthManagementDbContext dbContext, CompanyViewModel model)
        {
            _dbContext = dbContext;
            _model = model;
        }


        public void Handle()
        {
            _dbContext.Companies.Add(new Company{Name = _model.Name });
            _dbContext.SaveChanges();
        }
    }

}