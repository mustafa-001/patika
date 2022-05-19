using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.MovieOperations.CreateMovie;
using WebApi.MovieOperations.DeleteMovie;
using WebApi.MovieOperations.GetMovieDetail;
using WebApi.MovieOperations.GetMovies;
using WebApi.MovieOperations.UpdateMovie;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public MoviesController(IMovieStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public List<MoviesViewModel> Get()
    {
        GetMoviesQuery query = new GetMoviesQuery(_dbContext, _mapper);
        return query.Handle();
    }

    [HttpGet("{id}")]
    public MovieDetailModel GetMovieDetail(int id)
    {
        var query = new GetMovieDetailQuery(_dbContext, _mapper, id);
        return query.Handle();
    }
    [HttpPost]
    public IActionResult CreateMovie([FromBody] CreateMovieModel model)
    {
        var command = new CreateMovieCommand(_dbContext, _mapper, model);
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie([FromBody] UpdateMovieModel updateModel, int id)
    {
        var command = new UpdateMovieCommand(_dbContext, _mapper, updateModel, id);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var command = new DeleteMovieCommand(_dbContext, id);
        command.Handle();
        return Ok();

    }

}
