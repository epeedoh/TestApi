using EPE_AuthLibrary.Interfaces;
using EPE_AuthLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
           _userService = userService;
            _tokenService = tokenService;
        }

        //Enregistrement d'un utilisateur
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationModel model)
        {
            var newUser = new User
            {
                Username = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            await _userService.CreateUserAsync(newUser, model.Password);

            return Ok(new {Message = "Enregistrement effectué avec succes"});
        }

        //Connexion d'un utilisateur
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginModel model)
        {
            var user = await _userService.GetUserByUsernameOrEmailOrPhoneAsync(model.Username);
            if(user !=null && _userService.VerifyPassword(user, model.Password))
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(new {Token = token}); 
            }

            return Unauthorized("Login ou mot de passe incorrect");
        }

    }
}
