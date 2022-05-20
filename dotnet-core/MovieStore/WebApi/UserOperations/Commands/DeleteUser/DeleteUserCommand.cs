
using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public DeleteUserCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var User = _context.Users.SingleOrDefault(x => x.Id == UserId);
            if (User is null)
                throw new InvalidOperationException("Book User does not exits.");

            _context.Users.Remove(User);
            _context.SaveChanges();

        }

    }

}