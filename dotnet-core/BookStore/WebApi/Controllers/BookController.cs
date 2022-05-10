using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;

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
        public Book GetById(int id)
        {
            return _context.Books.Where(x => x.Id == id).SingleOrDefault();
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

            // if (book is not null)
            // {
            //     return BadRequest();
            // }
            // _context.Books.Add(newBook);
            // _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);
            if (book is null)
                return BadRequest();
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(book => book.Id == id);
            if (book is null)
                return BadRequest();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }


    }
}
