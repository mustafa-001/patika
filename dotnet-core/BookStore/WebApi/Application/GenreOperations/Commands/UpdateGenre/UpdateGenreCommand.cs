
using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int Id {get; set;}
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public UpdateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre =  _context.Genres.SingleOrDefault(x=> x.Id == Id);
            if (genre is null) 
            throw new InvalidOperationException("Book genre doesn't exists.");
            if ( _context.Genres.Any(x=> x.Name.ToLower() == Model.Name.ToLower()))
            throw new InvalidOperationException("Another genre with same name already exists.");
            genre.Name = Model.Name.Trim() == default ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }

    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}