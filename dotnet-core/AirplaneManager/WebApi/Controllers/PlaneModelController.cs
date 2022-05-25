using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.PlaneModelOperations;

namespace WebApi.Controllers.PlaneModelController
{


    [ApiController]
    [Route("[controller]s")]
    public class PlaneModelController : ControllerBase
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public PlaneModelController(IFligthManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public List<PlaneModelViewModel> GetPlaneModels()
        {
            GetPlaneModelsQuery query = new(_dbContext);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult CreatePlaneModel([FromBody] PlaneModelViewModel input)
        {
            var command = new CreatePlaneModelCommand(_dbContext,  input);
            command.Handle();
            return  Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlaneModel(int id)
        {
            var command = new DeletePlaneModelCommand(_dbContext, id);
            command.Handle();
            return Ok();
        }
    }
}