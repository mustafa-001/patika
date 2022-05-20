
using AutoMapper;
using System;
using WebApi.Entities;
using System.Linq;
using WebApi.DBOperations;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IMovieStoreDbContext context,  IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();
                return token;
            } else
            {
                throw new InvalidOperationException("Wrong username or password");
            }
        }
    }
    public class CreateTokenModel
    {
        public string Email {get; set;}  = null!;
        public string Password {get; set;} = null!;
    }
}