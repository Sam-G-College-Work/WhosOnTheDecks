using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using WhosOnTheDecks.API.Dtos;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Controllers
{
    //Auth controller created to authorise users registering and logging in
    //Inherits from the controller Base as thats where the underlying methods for url requests are
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DjsController : ControllerBase
    {
        //Property of IEventRepository is declared
        private readonly IEventRepository _erepo;

        //Constructor is used to insialise the repository property from above
        public DjsController(IEventRepository erepo)
        {
            _erepo = erepo;
        }

        //GetDjEvents will get all events the Dj has been booked for 
        [HttpGet("getdjevents/{id}")]
        public async Task<IActionResult> GetDjEvents(int id)
        {
            //Bookings are pulled from the database
            var bookings = await _erepo.GetBookings();

            //A list od events is created to hold all events related to dj
            List<Event> djEvents = new List<Event>();

            //A loop is perfomred to itterate therough all bookings in bookings
            foreach (Booking booking in bookings)
            {
                //A check is performed to see if the booking dj id matches the djid entered
                if (booking.DjId == id)
                {
                    //the event tied to the djs booking is pulled form the database
                    var ev = await _erepo.GetEvent(booking.EventId);

                    //The event is then added to the djsevent list
                    djEvents.Add(ev);
                }
            }
            //The list of djs events are returned with a staus of ok
            return Ok(djEvents);
        }

        //GetBooking will locate the booking related to the event the dj is booked for
        [HttpGet("booking/{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            //A Booking is pulled from the databse that maches the entered eventId
            var booking = await _erepo.GetBooking(id);

            //A booking display dto object is created
            //This will returnt he data in a format we wish to use in the front end
            BookingDisplayDto bdto = new BookingDisplayDto();

            bdto.BookingId = booking.BookingId;
            bdto.BookingStatus = booking.BookingStatus.ToString();
            bdto.EventId = booking.EventId;
            bdto.DjId = booking.DjId;

            //The display object is then returned with a status of ok
            return Ok(bdto);
        }

        //PostResponse method will take in an event id and a booking disply object containing the accepted or declined option
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PostResponse(int id, BookingDisplayDto booking)
        {
            //A booking is pulled form the databse that matches the entered event id
            var bookingToChange = await _erepo.GetBooking(id);

            //The bookings status is then changed 
            //If the dj ahs accepted
            if (booking.BookingStatus == "Accepted")
            {
                //Booking status is changed to accepted
                bookingToChange.BookingStatus = BookingStatus.Accepted;
            } // If the dj has declined
            else if (booking.BookingStatus == "Declined")
            {
                //Booking status is changed to declined
                bookingToChange.BookingStatus = BookingStatus.Declined;
            }
            else
            {
                //If the user manages to enter something else they are met with a bad request
                return BadRequest("Please select Accept or Decline");
            }

            //the booking is then updated in the databse
            _erepo.Update(bookingToChange);

            //The events databse is then saved
            await _erepo.SaveAll();

            //The booking is then returned with a status of ok 
            return Ok(bookingToChange);
        }
    }
}