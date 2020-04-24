using System.Collections.Generic;
using System.Threading.Tasks;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    //IPaymentRepository will outline 
    //the methods used to add edit retrieve and delete payment data
    public interface IPaymentRepository
    {
        //Add takes in any T of entity where it matches to a class
        void Add<T>(T entity) where T : class;

        //Save all return a true or false depending on outcome
        Task<bool> SaveAll();

        //Get Payments will retrive all payments in database
        Task<IEnumerable<Payment>> GetPayments();

        //Get Payment will retrive a single payment
        Task<Payment> GetPayment(int id);

    }
}