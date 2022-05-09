using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookStoreController : ControllerBase
    {

        private static List<Book> BookList = new List<Book>()
        {
            new Book{
                Id = 1,
                Title="Lean Startup",
                GenreId=1, //Personal Growth
                PageCount= 200,
                PublishDate = new DateTime(2001,12,1)
            },

            new Book{
                Id = 2,
                Title="Herland",
                GenreId=2, //Science Fiction
                PageCount= 250,
                PublishDate = new DateTime(2010,12,1)
            },
            new Book{
                Id = 3,
                Title="Dune",
                GenreId=2,
                PageCount= 200,
                PublishDate = new DateTime(2001,08,1)
            }

        };

         [HttpGet]
         public List<Book> GetBooks()
         {
             var  bookList = BookList.OrderBy(x=> x.Id).ToList<Book>();
             return bookList;
         }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            return BookList.Where(x => x.Id == id).SingleOrDefault();
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     return BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        // }


    }
}
