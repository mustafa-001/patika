using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Database;

namespace WebApi.AirfieldOperations
{
    public class GetAirfieldsQuery
    {
        private readonly IFligthManagementDbContext _dbContext;

        public GetAirfieldsQuery(IFligthManagementDbContext dbContext )
        {
            _dbContext = dbContext;
        }


        public List<AirfieldViewModel> Handle()
        {
            var Airfields =  _dbContext.Airfields.Select(x=> new AirfieldViewModel{Id = x.Id})
            .ToList();
            return Airfields;
        }
    }

    public class AirfieldViewModel
    {
        public string Id { get; set; }= String.Empty;
    }

}