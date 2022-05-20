using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.DirectorOperations.CreateDirector;
using WebApi.DirectorOperations.DeleteDirector;
using WebApi.DirectorOperations.GetDirectorDetail;
using WebApi.DirectorOperations.GetDirectors;
using WebApi.DirectorOperations.UpdateDirector;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DirectorsController : ControllerBase
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public DirectorsController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public List<DirectorsViewModel> Get()
    {
        GetDirectorsQuery query = new GetDirectorsQuery(_dbContext, _mapper);
        return query.Handle();
    }

    [HttpGet("{id}")]
    public DirectorDetailModel GetDirectorDetail(int id)
    {
        var query = new GetDirectorDetailQuery(_dbContext, _mapper, id);
        return query.Handle();
    }
    [HttpPost]
    public IActionResult CreateDirector([FromBody] CreateDirectorModel model)
    {
        var command = new CreateDirectorCommand(_dbContext, _mapper, model);
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDirector([FromBody] UpdateDirectorModel updateModel, int id)
    {
        var command = new UpdateDirectorCommand(_dbContext, _mapper, updateModel, id);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDirector(int id)
    {
        var command = new DeleteDirectorCommand(_dbContext, id);
        command.Handle();
        return Ok();

    }

}
