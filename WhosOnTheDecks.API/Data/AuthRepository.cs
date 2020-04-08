using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        //Adds datacontext which give acess to the database
        private readonly DataContext _context;

        //Constructor created to initialise the data being inject with the Datacontext property
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        //The login method takes in a string username and a string password
        //The methos will compare the details with the details stored in the databse
        //As the password is stored as a hash and salt within the database
        //the method will turn the string password into a hash and salt to compare to the stored data
        public async Task<User> Login(string username, string password)
        {
            //Create user property from the database
            //First or default will return null if the user does not exist
            //This will help by stopping an exceptionfrom being thrown
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            //This call is to check if the user does not exist by returning a null
            //This when seen by the front end application will show a 401 unauthorised
            if(user==null)
            {
                return null;
            }
                
            //This call is to check is the password matches or does not
            //Verify Password Hash method is called and passed the user object plus hash and salt
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            
            //if both previous check have been passed the user is verified and the user
            //object is returned
            return user;
        }

        //Varify password hash method takes in the string password and byte arrays for the hash and salt
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //hmac uses a cryptography library 
            //The using statement has been applied so that all data is disposed using the IDisposable interface
            //The password Salt is used as a key to verify the hash
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //The computed hash is created from the entered password and using the key produced above
                //will be the same as the stored hash in the database
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                //The for loop is used to check each component of the byte array for the hash
                //With the stored hash byte array
                for(int i = 0; i < computedHash.Length; i++)
                {
                    //Check used to return false if the passwords do not match
                    if(computedHash[i] != passwordHash[i])
                    { 
                        return false;
                    }
                }
            }

            return true;
        }

        //Takes in user object and the string as a password
        //Due to the sensitivity of the data a password hash and salt have been used to hide the data 
        public async Task<User> Register(User user, string password)
        {
            //Byte array is created to store both the hash and the salt as one
            byte[] passwordHash, passwordSalt;

            //CreatePasswordHash mthod is called and passed the hash and salt to store
            //the out keyword is used so only a reference to these variables is used
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            //Adds hash to user object
            user.PasswordHash = passwordHash;

            //Adds Salt to user object
            user.PasswordSalt = passwordSalt;

            //Adds new user and details to database
            await _context.Users.AddAsync(user);

            //Saves database
            await _context.SaveChangesAsync();

            //Returns a user object
            return user;
        }

        //Create password hash is called within register method
        //The method is void as it will only create a hash and a salt plus hmac key reference 
        //Takes in the password string and the 2 byte arrays created in the register method
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //hmac uses a cryptography library 
            //the library generates a key for the salt to unlock the hash related to it
            //The using statement has been applied so that the IDispose interface the library is 
            //connected to is used at the end of the method call 
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                
                passwordSalt = hmac.Key;

                //Compute hash method within hmac takes a byte array 
                //The string password is converted to a byte array 
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //User Exitis method takes in the string of username and 
        //compares it with any usernamesint he database that match
        public async Task<bool> UserExists(string username)
        {
            //Checks entered username with all usernames in database
            //If the username exists it returns true
            if (await _context.Users.AnyAsync(x => x.Username == username))
            {
                return true;
            }
            
            //If the above check has been passed the 
            //method call will return false and the user does not exist
            return false;        
        }
    }
}