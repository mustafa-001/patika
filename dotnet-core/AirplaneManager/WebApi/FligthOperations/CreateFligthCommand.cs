using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Entities;

namespace WebApi.FligthOperations
{
    public class CreateFligthCommand
    {
        private readonly IFligthManagementDbContext _dbContext;

        private readonly IMapper _mapper;
        private readonly FligthViewModel _model;

        public CreateFligthCommand(IFligthManagementDbContext dbContext, IMapper mapper, FligthViewModel model)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _model = model;
        }

        public void Handle()
        {
            var fligth = _mapper.Map<Fligth>(_model);

            //Note: If pilot's Attach'ed before fligth itself, efcore doesn't 
            //put fligth-pilot relationship. It works with Update or UpdateRange //thought. Is this a bug?
            foreach (var pilot in fligth.Pilots )
            {
                _dbContext.Attach(pilot);
                
            }
            _dbContext.Attach(fligth);
            _dbContext.Fligths.Add(fligth);
            _dbContext.SaveChanges();
        }
    }

}