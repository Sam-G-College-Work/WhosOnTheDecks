using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WhosOnTheDecks.API.Data;
using WhosOnTheDecks.API.Dtos;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Controllers
{
    //Auth controller created to authorise users registering and logging in
    //Inherits from the controller Base as thats where the underlying methods for url requests are
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Property _repo to initialise the IAuthRepository so Authorisations methods can be used
        private readonly IAuthRepository _repo;

        //Property _config to initialise the IConfiguration methods avaliable through IConfiguration
        private readonly IConfiguration _config;

        //Auth Constuctor 
        //Inistialising both _config and _repo by transferring in 2 arguments for repo and config
        //Access to the database and verification methods will now be avaliable to verify registering and login
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;

            _repo = repo;

        }

        //Register Method takes in a UserForRegisterDto contaiing the entered information from the data transfer object
        //The data transfer object is used to creat a simpler version of the user object 
        //This is so the data can be compared and displayed without having to pull and create the full user object 
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
             //Turns entered username to lowercase for easier verfications
            userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

            //Call to user exists method with passed in email
            //If returned true a bad request will be sent to the user
            if (await _repo.UserExists(userForRegisterDto.Email))
            {
                return BadRequest("Username already exists");
            }

            //Start building a user object with the username
            var userToCreate = new User
            {
                Email = userForRegisterDto.Email
            };

            //Complete the user object by adding the password 
            //submited and hashed through the register method 
            //of the IAuth Repository
            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            //Status code 201 "OK" sent back after completed request
            return StatusCode(201);
        }

        //Login method takes in the UserForLoginDto
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //Property is inialised with the stored information associated with the entered information
            var userFromRepo = await _repo.Login(userForLoginDto.Email.ToLower(), userForLoginDto.Password);

            //Check is made to see if the user exists
            //If null is returned the user does not exist
            //An unauthorized is returned
            if (userFromRepo == null)
            {
                return Unauthorized();
            }  

            //A token will be constructed with the users ID and email
            //This is stored in the array claims
            var claims = new[]
            {
                //Claim type name identifier is used to store the ID
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                //Claim type name is used to store the usename
                new Claim(ClaimTypes.Name, userFromRepo.Email)
            };

            //Key is created and hashed so it is not readable 
            //Key will be stored on server once deployed
            //For now the key is stored in appsettings.json 
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            //Creating credientals to sign in using the key
            //The method sign in credientals takes the key and
            //The hashing algorythm used to hash the key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Applying the credientals to the key
            //The claims are added from above
            //As well as the date they hahve signed in 
            //An expiry is added for 24hrs so the key will no longer work after then
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            //Create a token handler 
            var tokenHandler = new JwtSecurityTokenHandler();

            //Create the token by giving the tokenhanlder the information from above
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //A return of ok is given once all checks are passed
            //The token is also returned in the form of the token handler
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });

        }

    }
}