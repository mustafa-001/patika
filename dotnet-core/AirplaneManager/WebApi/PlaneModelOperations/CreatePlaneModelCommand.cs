using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.PlaneModelOperations
{
    public class CreatePlaneModelCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly PlaneModelViewModel _model;

        public CreatePlaneModelCommand(IFligthManagementDbContext dbContext, PlaneModelViewModel model)
        {
            _dbContext = dbContext;
            _model = model;
        }


        public void Handle()
        {
            _dbContext.PlaneModels.Add(new PlaneModel());
            _dbContext.SaveChanges();
        }
    }

}