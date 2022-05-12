
using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public DeleteAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var Author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (_context.Books.SingleOrDefault(x => x.Author.Id == AuthorId) is not null)
                throw new InvalidOperationException("This author still has other books");
            if (Author is null)
                throw new InvalidOperationException("Book Author does not exits.");

            _context.Authors.Remove(Author);
            _context.SaveChanges();

        }

    }

}