
using AutoMapper;
using System;
using WebApi.Entities;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            if( _context.Genres.SingleOrDefault(x=> x.Name == Model.Name) is not null) 
            throw new InvalidOperationException("Book genre already exists.");
            var genre = new Genre();
            genre.Name = Model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();

        }

    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}