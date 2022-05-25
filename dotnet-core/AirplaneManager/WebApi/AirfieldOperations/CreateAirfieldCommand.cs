using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.AirfieldOperations
{
    public class CreateAirfieldCommand
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly AirfieldViewModel _model;

        public CreateAirfieldCommand(IFligthManagementDbContext dbContext, AirfieldViewModel model)
        {
            _dbContext = dbContext;
            _model = model;
        }


        public void Handle()
        {
            _dbContext.Airfields.Add(new Airfield{Id = _model.Id });
            _dbContext.SaveChanges();
        }
    }

}