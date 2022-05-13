using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this IBookStoreDbContext context)
        {
            if (context.Books.Any())
                {
                    return;
                }

 
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

        }
    }
}