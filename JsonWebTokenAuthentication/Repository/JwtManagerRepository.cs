using JsonWebTokenAuthentication.IRepository;
using JsonWebTokenAuthentication.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebTokenAuthentication.Repository
{
    public class JwtManagerRepository : IJwtManagerRepository
    {
        private readonly IConfiguration configuration;
        public JwtManagerRepository(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            {"user1", "password1"},
            {"user2", "password2"},
            {"user3", "password3"},
        };

        public Tokens Authenticate(Users users)
        {
            if(!UserRecords.Any( x => x.Key==users.Name && x.Value == users.Password))
            {
                return null;
            }

            //else we generate JWT Token
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokenDescription);

            return new Tokens { Token = tokenhandler.WriteToken(token) };
        }
    }
}
