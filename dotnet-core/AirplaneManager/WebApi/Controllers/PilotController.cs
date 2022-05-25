using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.PilotOperations;

namespace WebApi.Controllers.PilotController
{


    [ApiController]
    [Route("[controller]s")]
    public class PilotController : ControllerBase
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public PilotController(IFligthManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public List<PilotViewModel> GetPilots()
        {
            GetPilotsQuery query = new(_dbContext);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult CreatePilot([FromBody] PilotViewModel input)
        {
            var command = new CreatePilotCommand(_dbContext, _mapper, input);
            command.Handle();
            return  Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePilot(int id)
        {
            var command = new DeletePilotCommand(_dbContext, id);
            command.Handle();
            return Ok();
        }
    }
}