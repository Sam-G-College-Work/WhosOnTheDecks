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

        [HttpGet("booking/{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _erepo.GetBooking(id);

            BookingDisplayDto bdto = new BookingDisplayDto();

            bdto.BookingId = booking.BookingId;
            bdto.BookingStatus = booking.BookingStatus.ToString();
            bdto.EventId = booking.EventId;
            bdto.DjId = booking.DjId;

            return Ok(bdto);
        }

    }
}