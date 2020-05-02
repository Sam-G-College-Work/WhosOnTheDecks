using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Controllers

{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {

        private readonly IEventRepository _repo;


        public BookingsController(IEventRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("geteventbookings/{id}")]
        public async Task<IActionResult> GetEventBookings(int Id)
        {
            var bookings = await _repo.GetBookings();

            Booking eventBooking = new Booking();

            foreach (Booking booking in bookings)
            {
                if (booking.EventId == Id)
                {
                    eventBooking.Equals(booking);
                }
            }

            return Ok(eventBooking);
        }

        [HttpGet("getdjbookings/{id}")]
        public async Task<IActionResult> GetDjBookings(int Id)
        {
            var bookings = await _repo.GetBookings();

            List<Booking> djbookings = new List<Booking>();

            foreach (Booking booking in bookings)
            {
                if (booking.DjId == Id)
                {
                    djbookings.Add(booking);
                }
            }

            return Ok(djbookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _repo.GetBooking(id);

            return Ok(booking);
        }

        [HttpGet("bookingexists")]
        public async Task<IActionResult> BookingExists(int DjId, int EventId)
        {
            bool bookingExists = await _repo.BookingExists(DjId, EventId);

            return Ok(bookingExists);
        }

    }
}