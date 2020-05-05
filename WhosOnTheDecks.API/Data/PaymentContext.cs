using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    //Payment Context will provide all Database methods to create, update, retrieve and delete payments
    public class PaymentContext : IPaymentRepository
    {
        //Property created to access databse
        private readonly DataContext _context;

        //Contructor accepts datacontext as argument and insitialise is to the 
        //above property
        public PaymentContext(DataContext context)
        {
            _context = context;
        }

        //Add will take in any type of entity that matches a class
        //The entity will be added from the databse 
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        //Add will take in any type of entity that matches a class
        //The entity will be added from the databse 
        public void Remove(Payment payment)
        {
            _context.Remove(payment);
        }

        //GetPayments will retunr a list of Payments
        public async Task<IEnumerable<Payment>> GetPayments()
        {
            var payments = await _context.Payments.ToListAsync();

            return payments;
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