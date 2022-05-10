using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.DeleteBook;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookStoreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookStoreController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<BooksViewModel> GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            return query.Handle();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, id);
            try
            {
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
            var command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            var command = new UpdateBookCommand(_context);
            try
            {
                command.Model = updatedBook;
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
