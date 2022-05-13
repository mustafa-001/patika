
using AutoMapper;
using System;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public DeleteUserCommand(IBookStoreDbContext context, IMapper mapper)
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