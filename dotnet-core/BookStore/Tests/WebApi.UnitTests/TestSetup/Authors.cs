
using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this IBookStoreDbContext context)
        {
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
        }
    }
}

