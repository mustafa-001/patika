using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database;
using WebApi.CompanyOperations;

namespace WebApi.Controllers.CompanyController
{


    [ApiController]
    [Route("[controller]s")]
    public class CompanyController : ControllerBase
    {
        private readonly IFligthManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public CompanyController(IFligthManagementDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public List<CompanyViewModel> GetCompanys()
        {
            GetCompaniesQuery query = new(_dbContext);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyViewModel input)
        {
            var command = new CreateCompanyCommand(_dbContext,  input);
            command.Handle();
            return  Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(string id)
        {
            var command = new DeleteCompanyCommand(_dbContext, id);
            command.Handle();
            return Ok();
        }
    }
}