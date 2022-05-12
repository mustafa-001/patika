
using AutoMapper;
using System;
using WebApi.Entities;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            if( _context.Authors.SingleOrDefault(x=> x.Name == Model.Name) is not null) 
            throw new InvalidOperationException("Book Author already exists.");
            var Author = new Author();
            Author.Name = Model.Name;
            _context.Authors.Add(Author);
            _context.SaveChanges();

        }

    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}