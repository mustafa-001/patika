
using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel Model { get; set; }
        public int Id {get; set;}
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author =  _context.Authors.SingleOrDefault(x=> x.Id == Id);
            if (author is null) 
            throw new InvalidOperationException("Author doesn't exists.");
            if ( _context.Authors.Any(x=> x.Name.ToLower() == Model.Name.ToLower()))
            throw new InvalidOperationException("Another Author with same name already exists.");
            author.Name = Model.Name.Trim() == default ? author.Name : Model.Name;
            author.BirthDate = Model.BirthDate == default ?  author.BirthDate : Model.BirthDate;
            _context.SaveChanges();
        }

    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public DateTime BirthDate {get; set;}
    }
}