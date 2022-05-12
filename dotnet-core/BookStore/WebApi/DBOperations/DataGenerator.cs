using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {

                if (context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "J.R.R. Tolkien",
                        BirthDate = new DateTime(1940, 1, 1),
                    },
                    new Author
                    {
                        Name = "Mary Shelby",
                        BirthDate = new DateTime(1800, 1, 1)
                    },
                    new Author
                    {
                        Name = "Frank Herbert",
                        BirthDate = new DateTime(1800, 1, 1)
                    }
                );


                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Novel"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Id = 1,
                        Title = "LOTR",
                        GenreId = 1, //Personal Growth
                        AuthorId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 12, 1)
                    },

                    new Book
                    {
                        Id = 2,
                        Title = "Frankenstein",
                        GenreId = 2, //Science Fiction
                        AuthorId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 12, 1)
                    },
                    new Book
                    {
                        Id = 3,
                        Title = "Dune",
                        GenreId = 2,
                        AuthorId = 3,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 08, 1)
                    }
                );
                context.SaveChanges();

            }
        }
    }
}