using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.PlaneOperations
{
    public class CreatePlaneCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly PlaneViewModel _model;

        public CreatePlaneCommand(IFligthManagementDbContext dbContext, PlaneViewModel model)
        {
            _dbContext = dbContext;
            _model = model;
        }


        public void Handle()
        {
            _dbContext.Planes.Add(new Plane{Id = _model.Id });
            _dbContext.SaveChanges();
        }
    }

}