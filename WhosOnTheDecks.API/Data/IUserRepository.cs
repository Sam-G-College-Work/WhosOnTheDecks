using System.Collections.Generic;
using System.Threading.Tasks;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    //IUserRepository will outline 
    //the methods used to add edit retrieve and delete user data
    public interface IUserRepository
    {
        //Add takes in any T of entity where it matches to a class
        void Add<T>(T entity) where T : class;

        //Delete removes any T of entity where it matches a class
        void Delete<T>(T entity) where T : class;

        //Save all return a true or false depending on outcome
        Task<bool> SaveAll();

        //Get Djs will retrive all Djs in database
        Task<IEnumerable<Dj>> GetDjs();

        //Get Dj will retrive a single Dj
        Task<Dj> GetDj(int id);

        //Get Promoter will retrive a single promoter
        Task<Promoter> GetPromoter(int id);

    }
}