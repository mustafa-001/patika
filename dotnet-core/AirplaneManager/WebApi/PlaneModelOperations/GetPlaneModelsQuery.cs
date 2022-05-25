using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;

namespace WebApi.PlaneModelOperations
{
    public class GetPlaneModelsQuery
    {
        private readonly IFligthManagementDbContext _dbContext;

        public GetPlaneModelsQuery(IFligthManagementDbContext dbContext )
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