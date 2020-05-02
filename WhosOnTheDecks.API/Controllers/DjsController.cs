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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DjsController : ControllerBase
    {
        private readonly IEventRepository _erepo;

        public DjsController(IEventRepository erepo)
        {
            _erepo = erepo;
        }

        [HttpGet("getdjbookings/{id}")]
        public async Task<IActionResult> GetDjBookings(int Id)
        {
            var bookings = await _erepo.GetBookings();

            List<Booking> djBookings = new List<Booking>();

            foreach (Booking booking in bookings)
            {
                if (booking.DjId == Id)
                {
                    djBookings.Add(booking);
                }
            }
            return Ok(djBookings);
        }

        [HttpGet("getdjevents/{id?}")]
        public async Task<IActionResult> GetDjEvents(int id)
        {
            var bookings = await _erepo.GetBookings();

            List<Event> djEvents = new List<Event>();

            foreach (Booking booking in bookings)
            {
                if (booking.DjId == id)
                {
                    var ev = await _erepo.GetEvent(booking.EventId);

                    djEvents.Add(ev);
                }
            }
            return Ok(djEvents);
        }

    }
}