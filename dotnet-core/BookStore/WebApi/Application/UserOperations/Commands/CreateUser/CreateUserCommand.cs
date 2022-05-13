
using AutoMapper;
using System;
using WebApi.Entities;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            if( _context.Users.SingleOrDefault(x=> x.Name == Model.Name) is not null) 
            throw new InvalidOperationException("User already exists.");
            var user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}