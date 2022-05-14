using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.MovieOperations.GetMovies;

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
}
