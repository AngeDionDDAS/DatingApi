using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using DatingApi.Data;
using DatingApi.Dto;
using DatingApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DatingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            userRegisterDto.UserName = userRegisterDto.UserName.ToLower();
            if (await _repo.UserExists(userRegisterDto.UserName))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                UserName = userRegisterDto.UserName,
            };
            var createdUser = await _repo.Register(userToCreate, userRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {          

                var userFromRepo = await _repo.Login(userLogin.UserName.ToLower(), userLogin.Password);
                if (userFromRepo == null)
                    return Unauthorized();
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userLogin.UserName)

            };

                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });




        } 

    }
}
