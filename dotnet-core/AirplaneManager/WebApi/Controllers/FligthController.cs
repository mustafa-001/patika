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
        public List<FligthDetailsViewModel> GetFligths()
        {
            GetFligthsQuery query = new(_dbContext, _mapper);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult CreateFligth([FromBody] FligthViewModel input)
        {
            var command = new CreateFligthCommand(_dbContext, _mapper, input);
            command.Handle();
            return  Ok();
        }
    }
}