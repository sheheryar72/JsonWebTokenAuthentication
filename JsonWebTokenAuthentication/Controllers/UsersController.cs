using JsonWebTokenAuthentication.IRepository;
using JsonWebTokenAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonWebTokenAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IJwtManagerRepository jwtManagerRepository;
            
        public UsersController(IJwtManagerRepository _jwtManagerRepository)
        {
            jwtManagerRepository = _jwtManagerRepository;
        }
        [HttpGet]
        [Route("UserList")]
        public IActionResult Get()
        {
            if (HttpContext.Session.GetString("AuthenticationToken") != null)
            {
                var users = new List<string>
            {
                "Sheheryar",
                "Ali",
                "Muneeb"
            };
                return Ok(users);
            }
            else
            {
                return Ok();
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(Users users)
        {
            var token = jwtManagerRepository.Authenticate(users);

            HttpContext.Session.SetString("AuthenticationToken", token.ToString());

            if(token == null)
            {
                return Unauthorized();
            }
            
            return Ok(token);
        }
    }
}
