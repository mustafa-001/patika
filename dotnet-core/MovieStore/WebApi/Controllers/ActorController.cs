using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.ActorOperations.CreateActor;
using WebApi.ActorOperations.DeleteActor;
using WebApi.ActorOperations.GetActorDetail;
using WebApi.ActorOperations.GetActors;
using WebApi.ActorOperations.UpdateActor;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorsController : ControllerBase
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public ActorsController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public List<ActorsViewModel> Get()
    {
        GetActorsQuery query = new GetActorsQuery(_dbContext, _mapper);
        return query.Handle();
    }

    [HttpGet("{id}")]
    public ActorDetailModel GetActorDetail(int id)
    {
        var query = new GetActorDetailQuery(_dbContext, _mapper, id);
        return query.Handle();
    }
    [HttpPost]
    public IActionResult CreateActor([FromBody] CreateActorModel model)
    {
        var command = new CreateActorCommand(_dbContext, _mapper, model);
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateActor([FromBody] UpdateActorModel updateModel, int id)
    {
        var command = new UpdateActorCommand(_dbContext, _mapper, updateModel, id);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteActor(int id)
    {
        var command = new DeleteActorCommand(_dbContext, id);
        command.Handle();
        return Ok();

    }

}
