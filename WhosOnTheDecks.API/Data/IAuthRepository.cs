using System.Threading.Tasks;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    //Interface is created to allow the retrieval and checking of user data
    //This has been created to allow multiple uses of the same methods across different controllers
    public interface IAuthRepository
    {
        //Register takes the user object and password as a string to register a new user
        Task<User> Register(User user, string password);

        //Login takes in a username and password as strings to verify details
        Task<User> Login(string username, string password);
        
        //UserExists will check the usename to see if it exists in the databse already
        Task<bool> UserExists(string username);
    }
}