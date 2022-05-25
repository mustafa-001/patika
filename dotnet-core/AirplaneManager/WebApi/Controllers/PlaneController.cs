using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.PlaneOperations;

namespace WebApi.Controllers.PlaneController
{


    [ApiController]
    [Route("[controller]s")]
    public class PlaneController : ControllerBase
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public PlaneController(IFligthManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public List<PlaneViewModel> GetPlanes()
        {
            GetPlanesQuery query = new(_dbContext);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult CreatePlane([FromBody] PlaneViewModel input)
        {
            var command = new CreatePlaneCommand(_dbContext,  input);
            command.Handle();
            return  Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlane(int id)
        {
            var command = new DeletePlaneCommand(_dbContext, id);
            command.Handle();
            return Ok();
        }
    }
}