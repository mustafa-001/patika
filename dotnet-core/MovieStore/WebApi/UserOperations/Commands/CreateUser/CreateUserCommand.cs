
using AutoMapper;
using System;
using WebApi.Entities;
using System.Linq;
using WebApi.DBOperations;
namespace WebApi.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public CreateUserCommand(IMovieStoreDbContext context, IMapper mapper, CreateUserModel model)
        {
            _context = context;
            _mapper = mapper;
            Model = model;
        }

        public void Handle()
        {
            if( _context.Users.SingleOrDefault(x=> x.Name == Model.Name) is not null) 
            throw new InvalidOperationException("User already exists.");
            var user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            user.Movies.Add(_context.Movies.First(x=> true));
            _context.SaveChanges();
        }

    }

    public class CreateUserModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Surname { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}