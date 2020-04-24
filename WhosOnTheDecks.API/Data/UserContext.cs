using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    //User Context will provide all Database methods to create, update, retrieve and delete users
    public class UserContext : IUserCrudRepository
    {
        //Property created to access databse
        private readonly DataContext _context;

        //Contructor accepts datacontext as argument and insitialise is to the 
        //above property
        public UserContext(DataContext context)
        {
            _context = context;
        }

        //Add will take in any type of entity that matches a class
        //The entity will be added from the databse 
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        //Delete will take in any type of entity that matches a class
        //The entity will be removed from the databse 
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        //GetDj will take in an integer of id and returnt he matching Dj
        //from the database
        public async Task<Dj> GetDj(int id)
        {
            //.Include(p => p.Photos) after user and before FirstOrDefault
            //if photos added by reference later
            var dj = await _context.Djs.FirstOrDefaultAsync(u => u.Id == id);

            return dj;
        }

        //Getdjs will retunr a list of Djs
        public async Task<IEnumerable<Dj>> GetDjs()
        {
            var djs = await _context.Djs.ToListAsync();

            return djs;
        }

        //GetPromoter will take in an integer of id and returnt he matching promoter
        //from the database
        public async Task<Promoter> GetPromoter(int id)
        {
            var promoter = await _context.Promoters.FirstOrDefaultAsync(u => u.Id == id);

            return promoter;
        }

        //GetPromoters will return a list of promoters
        public async Task<IEnumerable<Promoter>> GetPromoters()
        {
            var promoters = await _context.Promoters.ToListAsync();

            return promoters;
        }

        //GetStaff will take in an integer of id and returnt he matching staff
        //member from the database
        public async Task<Staff> GetStaff(int id)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(u => u.Id == id);

            return staff;
        }

        //GetStaffs will return a list of staff members
        public async Task<IEnumerable<Staff>> GetStaffs()
        {
            var staff = await _context.Staff.ToListAsync();

            return staff;
        }

        //SaveAll will save any changes to context resulting in the number
        //being greater than 0 and therefore true
        //if there are no changes the number will be equal to 0
        //and will return false
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}