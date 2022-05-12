
using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public DeleteGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var  genre =  _context.Genres.SingleOrDefault(x=> x.Id == GenreId);
            if (genre is null) 
            throw new InvalidOperationException("Book genre does not exits.");

            _context.Genres.Remove(genre);
            _context.SaveChanges();

        }

    }

}