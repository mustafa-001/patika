using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using FluentValidation;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public List<GenresViewModel> GetGenres()
        {
            var query = new GetGenresQuery(_context, _mapper);
            return query.Handle();
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreDetail(int id)
        {
            var query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            new GetGenreDetailsQueryValidator().ValidateAndThrow(query);
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel model)
        {
            var command = new CreateGenreCommand(_context, _mapper);
            command.Model = model;
            new CreateGenreCommandValidator().ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel model)
        {
            var command = new UpdateGenreCommand(_context, _mapper);
            command.Id = id;
            command.Model = model;
            new UpdateGenreCommandValidator().ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var command = new DeleteGenreCommand(_context, _mapper);
            command.GenreId = id;
            new DeleteGenreCommandValidator().ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


    }
}