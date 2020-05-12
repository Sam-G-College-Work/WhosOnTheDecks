using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using WhosOnTheDecks.API.Dtos;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Controllers
{
    //Promoter controller created to perform actions related to the user type promoter
    //Inherits from the controller Base as thats where the underlying methods for url requests are
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotersController : ControllerBase
    {
        //Property of IEventRepository is declared
        private readonly IEventRepository _erepo;

        //Property of IUserRepository is declared
        private readonly IUserRepository _urepo;

        //Constructor inisialises both Repositories to allow DBSet access
        public PromotersController(IEventRepository erepo,
        IUserRepository urepo)
        {
            _urepo = urepo;
            _erepo = erepo;
        }

        //GetPromoterEvents takes in the promoters Id and locates all events created by that promoter
        [HttpGet("events/{id}")]
        public async Task<IActionResult> GetPromoterEvents(int id)
        {
            //Events are pulled from the databse
            var events = await _erepo.GetEvents();

            //A list is created to take a copy of the events IEnumerable from above
            List<Event> promoterEvents = new List<Event>();

            //A for loop is started to itterate through all events in the events dbset
            foreach (Event ev in events)
            {
                //A booking related to the event is pulled from the db
                //This is required as no booking will exist if the event has not been paid for
                var bookingCheck = await _erepo.GetBooking(ev.EventId);

                //Any booking that does not exist will not be checked 
                //This prevents a null reference error from occuring
                if (bookingCheck != null)
                {
                    //A final check is perfomrmed to match the valid event to the promoter
                    //And adds it to the list
                    if (ev.PromoterId == id)
                    {
                        promoterEvents.Add(ev);
                    }
                }
            }
            //The list is then returned as part of an Ok status
            return Ok(promoterEvents);
        }

        //GetBookedDj will take in an event ID from the selected event by the user
        //This will return the booked DJ and their details 
        [HttpGet("dj/{id}")]
        public async Task<IActionResult> GetBookedDj(int id)
        {
            //Booking is pulled from the databse that matches the event Id supplied
            var booking = await _erepo.GetBooking(id);

            //A djid variable is created from the found booking and its related djid
            var djId = booking.DjId;

            //A dj is then pulled from the Database that matches the DjId from above
            var dj = await _urepo.GetDj(djId);

            //A new Dj Display Dto object is created
            //This is used to remove all sensitive data from dj object when sending it to the front end
            DjDisplayDto djdto = new DjDisplayDto();

            djdto.DjId = dj.Id;
            djdto.DjName = dj.DjName;
            djdto.Equipment = dj.Equipment;
            djdto.HourlyRate = dj.HourlyRate;
            djdto.Genre = dj.Genre.ToString();

            //The djdto object is then returned with an Ok status
            return Ok(djdto);
        }

        //GetBooking will take in an event id and fond a matching booking
        [HttpGet("booking/{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            //Booking is pulled from database using the event id
            var booking = await _erepo.GetBooking(id);

            //BookingDisplayDto object is created this is used
            //To display the event model in a desired way
            BookingDisplayDto bdto = new BookingDisplayDto();

            bdto.BookingId = booking.BookingId;
            bdto.BookingStatus = booking.BookingStatus.ToString();
            bdto.EventId = booking.EventId;
            bdto.DjId = booking.DjId;

            //Booking dto is retunred with ok status
            return Ok(bdto);
        }

    }
}