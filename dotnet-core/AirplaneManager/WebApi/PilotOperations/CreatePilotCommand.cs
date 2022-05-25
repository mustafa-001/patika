using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.PilotOperations
{
    public class CreatePilotCommand
    {
        private readonly IFligthManagementDbContext _dbContext;

        public CreatePilotCommand(IFligthManagementDbContext dbContext )
        {
            _dbContext = dbContext;
        }


        public void Handle()
        {
            _dbContext.Pilots.Add(new Pilot());
            _dbContext.SaveChanges();
        }
    }

}