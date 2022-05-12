using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public List<AuthorsViewModel> GetAuthors()
        {
            var query = new GetAuthorsQuery(_context, _mapper);
            return query.Handle();
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorDetail(int id)
        {
            var query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            new GetAuthorDetailsQueryValidator().ValidateAndThrow(query);
            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel model)
        {
            var command = new CreateAuthorCommand(_context, _mapper);
            command.Model = model;
            new CreateAuthorCommandValidator().ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel model)
        {
            var command = new UpdateAuthorCommand(_context, _mapper);
            command.Id = id;
            command.Model = model;
            new UpdateAuthorCommandValidator().ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var command = new DeleteAuthorCommand(_context, _mapper);
            command.AuthorId = id;
            new DeleteAuthorCommandValidator().ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }


    }
}