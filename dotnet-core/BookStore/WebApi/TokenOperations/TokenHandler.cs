using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration {get; set;}
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey  signingCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            tokenModel.Expiration = System.DateTime.Now.AddMinutes(15);
            SigningCredentials credentials = new SigningCredentials(signingCredentials, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials
            );
            tokenModel.AccessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            tokenModel.RefreshToken = CreateAccessToken();
            return tokenModel;
        }

        private string CreateAccessToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}