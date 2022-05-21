using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.FligthOperations;

namespace WebApi.Controllers.FligthController
{


    [ApiController]
    [Route("[controller]")]
    public class FligthController : ControllerBase
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public FligthController(IFligthManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public List<FligthViewModel> GetFligths()
        {
            GetFligthsQuery query = new(_dbContext, _mapper);
            return query.Handle();
        }
    }
}