using System.Collections.Generic;
using System.Threading.Tasks;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Data
{
    //IEventRepository will outline 
    //the methods used to add edit retrieve and delete event/booking data
    public interface IEventRepository
    {
        //Add takes in any T of entity where it matches to a class
        void Add<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;

        //Save all return a true or false depending on outcome
        Task<bool> SaveAll();

        //Get Events will retrive all events in database
        Task<IEnumerable<Event>> GetEvents();

        //Get Bookings will retrive all bookings in database
        Task<IEnumerable<Booking>> GetBookings();

        //Get Event will retrive a single event
        Task<Event> GetEvent(int id);

        //Get Booking will retrive a single booking
        Task<Booking> GetBooking(int id);

        //BookingExists will take in DjId and EventID
        //This will be used to verify if a Dj is already booked for an event that day
        Task<bool> BookingExists(int DjId, int EventId);
    }
}