
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.UserOperations.Queries.GetBougthMovies
{
    public class GetBougthMovies
    {
        private readonly string UserEmail;
        private readonly IMovieStoreDbContext _context;

        public GetBougthMovies(IMovieStoreDbContext context,  string userEmail)
        {
            _context = context;
            UserEmail = userEmail;
        }


        public List<int> Handle()
        {
            var user = _context.Users.Include(x=>x.Movies).SingleOrDefault(x => x.Email == UserEmail);

            if (user is null)
                throw new InvalidOperationException("User does not exits.");

            return  user.Movies.Select(x=> x.Id).ToList();
        }
    }


}