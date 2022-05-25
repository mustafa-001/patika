using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.AirfieldOperations;

namespace WebApi.Controllers.AirfieldController
{


    [ApiController]
    [Route("[controller]s")]
    public class AirfieldController : ControllerBase
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public AirfieldController(IFligthManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public List<AirfieldViewModel> GetAirfields()
        {
            GetAirfieldsQuery query = new(_dbContext);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult CreateAirfield([FromBody] AirfieldViewModel input)
        {
            var command = new CreateAirfieldCommand(_dbContext,  input);
            command.Handle();
            return  Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAirfield(string id)
        {
            var command = new DeleteAirfieldCommand(_dbContext, id);
            command.Handle();
            return Ok();
        }
    }
}