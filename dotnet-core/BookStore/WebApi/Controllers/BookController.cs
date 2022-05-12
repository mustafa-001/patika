using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.DeleteBook;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookStoreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookStoreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<BooksViewModel> GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            return query.Handle();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            query.BookId = id;
            try
            {
                validator.ValidateAndThrow(query);
                return Ok(query.Handle());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     return _context.Books.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        // }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            var command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            var command = new UpdateBookCommand(_context, _mapper);
            try
            {
                command.Model = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


    }
}
