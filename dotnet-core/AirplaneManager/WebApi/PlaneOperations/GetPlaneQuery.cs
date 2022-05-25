using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;

namespace WebApi.PlaneOperations
{
    public class GetPlanesQuery
    {
        private readonly IFligthManagementDbContext _dbContext;

        public GetPlanesQuery(IFligthManagementDbContext dbContext )
        {
            _dbContext = dbContext;
        }


        public List<PlaneViewModel> Handle()
        {
            var Planes =  _dbContext.Planes.Select(x=> new PlaneViewModel{Id = x.Id})
            .ToList();
            return Planes;
        }
    }

    public class PlaneViewModel
    {
        public int Id { get; set; }
    }

}