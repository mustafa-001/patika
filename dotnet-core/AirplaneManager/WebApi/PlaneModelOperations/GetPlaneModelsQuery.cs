using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;

namespace WebApi.PlaneModelOperations
{
    public class GetCompaniesQuery
    {
        private readonly IFligthManagementDbContext _dbContext;

        public GetCompaniesQuery(IFligthManagementDbContext dbContext )
        {
            _dbContext = dbContext;
        }


        public List<PlaneModelViewModel> Handle()
        {
            var PlaneModels =  _dbContext.PlaneModels.Select(x=> new PlaneModelViewModel{Id= x.Id})
            .ToList();
            return PlaneModels;
        }
    }

    public class PlaneModelViewModel
    {
        public int Id { get; set; }
    }

}