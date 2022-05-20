using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.DBOperations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using WebApi.UserOperations.Commands.CreateUser;
using WebApi.UserOperations.Commands.CreateToken;
using WebApi.TokenOperations.Models;
using WebApi.UserOperations.Commands.RefreshToken;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration ;


        public UserController(IMovieStoreDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            var command = new CreateUserCommand(_context, _mapper, newUser);
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            var command = new CreateTokenCommand(_context, _configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }
        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            var command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            var accessToken = command.Handle();
            return accessToken;
        }
    }
}