using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Concerns;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        // POST api/<UsersController>
        [HttpPost("Signup")]
        public bool SignUp([FromBody] User newUser)
        {
            bool userExists =_userService.AddUser(newUser);
            if(userExists)
                return false;
            else
                return true;
        }

        [HttpPost("Signin")]
        public IActionResult SignIn([FromBody] User currentUser)
        {
            var user = _userService.VerifyUser(currentUser);

            if (user != null)
            {
                var token = GenerateJwtToken(user);

                return Ok(new { Token = token});
            }
            return Unauthorized();

        }

        private string GenerateJwtToken(User user)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()) 
            };
            var token = new JwtSecurityToken(
                issuer:_configuration["Jwt:Issuer"], 
                audience:_configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
