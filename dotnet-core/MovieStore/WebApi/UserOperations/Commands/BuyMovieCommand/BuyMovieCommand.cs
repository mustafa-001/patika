
using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.UserOperations.Commands.BuyMovieCommand
{
    public class BuyMovieCommand
    {
        private readonly BuyMovieModel  _model;
        private readonly IMovieStoreDbContext _context;

        public BuyMovieCommand(IMovieStoreDbContext context,  BuyMovieModel model)
        {
            _context = context;
            _model = model;
        }

        public int MovieId { get; set; }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == _model.UserEmail);
            var movie =  _context.Movies.SingleOrDefault(x => x.Id == _model.MovieId);
            if (user is null)
                throw new InvalidOperationException("User does not exits.");

            if (movie is null)
                throw new InvalidOperationException("Movie does not exits.");

            user.Movies.Add(movie);
            _context.SaveChanges();
        }
    }

    public class BuyMovieModel
    {
        public int MovieId {get; set;}
        public string UserEmail {get; set;} = String.Empty;
    }

}