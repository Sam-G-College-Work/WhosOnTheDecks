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
        //Property _arepo to initialise the IAuthRepository so Authorisations methods can be used
        private readonly IAuthRepository _arepo;

        //Property _config to initialise the IConfiguration methods avaliable through IConfiguration
        private readonly IConfiguration _config;

        //Auth Constuctor 
        //Inistialising both _config and _arepo by transferring in 2 arguments for repo and config
        //Access to the database and verification methods will now be avaliable to verify registering and login
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;

            _arepo = repo;

        }

        //PromoterRegister Method takes in a PromoterForRegisterDto contaiing the entered information from the data transfer object
        //The data transfer object is used to create a simpler version of the promoter object 
        //This is so the data can be compared and displayed without having to pull and create the full user object 
        [HttpPost("promoterregister")]
        public async Task<IActionResult> PromoterRegister(PromoterForRegisterDto promoterForRegisterDto)
        {
            //Turns entered username to lowercase for easier verfications
            promoterForRegisterDto.Email = promoterForRegisterDto.Email.ToLower();

            //Call to user exists method with passed in email
            //If returned true a bad request will be sent to the user
            if (await _arepo.UserExists(promoterForRegisterDto.Email))
            {
                return BadRequest("Email already exists");
            }

            //Start building a user object with the username
            var promoterToCreate = new Promoter
            {
                Email = promoterForRegisterDto.Email,
                FirstName = promoterForRegisterDto.FirstName,
                LastName = promoterForRegisterDto.LastName,
                LockAccount = false,
                HouseNameOrNumber = promoterForRegisterDto.HouseNameOrNumber,
                StreetName = promoterForRegisterDto.StreetName,
                Postcode = promoterForRegisterDto.Postcode,
                PhoneNumber = promoterForRegisterDto.PhoneNumber,
                Role = Role.Promoter,
                CompanyName = promoterForRegisterDto.CompanyName
            };


            //Complete the user object by adding the password 
            //submited and hashed through the register method 
            //of the IAuth Repository
            var createdPromoter = await _arepo.Register(promoterToCreate, promoterForRegisterDto.Password);

            //Status code 201 "OK" sent back after completed request
            return StatusCode(201);
        }

        //DjRegister Method takes in a DjForRegisterDto contaiing the entered information from the data transfer object
        //The data transfer object is used to create a simpler version of the Dj object 
        //This is so the data can be compared and displayed without having to pull and create the full user object 
        [HttpPost("djregister")]
        public async Task<IActionResult> DjRegister(DjForRegisterDto djForRegisterDto)
        {
            //Turns entered username to lowercase for easier verfications
            djForRegisterDto.Email = djForRegisterDto.Email.ToLower();

            //Call to user exists method with passed in email
            //If returned true a bad request will be sent to the user
            if (await _arepo.UserExists(djForRegisterDto.Email))
            {
                return BadRequest("Email already exists");
            }

            //Start building a user object with the username
            var djToCreate = new Dj
            {
                Email = djForRegisterDto.Email,
                FirstName = djForRegisterDto.FirstName,
                LastName = djForRegisterDto.LastName,
                LockAccount = true,
                HouseNameOrNumber = djForRegisterDto.HouseNameOrNumber,
                StreetName = djForRegisterDto.StreetName,
                Postcode = djForRegisterDto.Postcode,
                PhoneNumber = djForRegisterDto.PhoneNumber,
                Role = Role.Dj,
                DjName = djForRegisterDto.DjName,
                HourlyRate = djForRegisterDto.HourlyRate,
                Equipment = djForRegisterDto.Equipment,
                Genre = djForRegisterDto.Genre
            };


            //Complete the user object by adding the password 
            //submited and hashed through the register method 
            //of the IAuth Repository
            var createdDj = await _arepo.Register(djToCreate, djForRegisterDto.Password);

            //Status code 201 "OK" sent back after completed request
            return StatusCode(201);
        }

        //Login method takes in the UserForLoginDto
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //Property is inialised with the stored information associated with the entered information
            var userFromRepo = await _arepo.Login(userForLoginDto.Email.ToLower(), userForLoginDto.Password);

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
                new Claim(ClaimTypes.Email, userFromRepo.Email),
                //Claim type role is used to store users role in Jason web token
                new Claim(ClaimTypes.Role, userFromRepo.Role.ToString())
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
            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }

    }
}