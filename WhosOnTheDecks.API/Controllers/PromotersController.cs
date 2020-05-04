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
    public class PromotersController : ControllerBase
    {
        private readonly IEventRepository _erepo;
        private readonly IUserRepository _urepo;

        public PromotersController(IEventRepository erepo,
        IUserRepository urepo)
        {
            _urepo = urepo;
            _erepo = erepo;
        }

        [HttpGet("events/{id}")]
        public async Task<IActionResult> GetPromoterEvents(int id)
        {
            var events = await _erepo.GetEvents();

            List<Event> promoterEvents = new List<Event>();

            foreach (Event ev in events)
            {
                if (ev.PromoterId == id)
                {
                    promoterEvents.Add(ev);
                }
            }
            return Ok(promoterEvents);
        }

        [HttpGet("dj/{id}")]
        public async Task<IActionResult> GetBookedDj(int id)
        {
            var booking = await _erepo.GetBooking(id);

            var djId = booking.DjId;

            var dj = await _urepo.GetDj(djId);

            DjDisplayDto djdto = new DjDisplayDto();

            djdto.DjId = dj.Id;
            djdto.DjName = dj.DjName;
            djdto.Equipment = dj.Equipment;
            djdto.HourlyRate = dj.HourlyRate;
            djdto.Genre = dj.Genre.ToString();

            return Ok(djdto);
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