using System;
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
    public class CreateEventsController : ControllerBase
    {
        private readonly IEventRepository _erepo;
        private readonly IUserRepository _urepo;
        private readonly IPaymentRepository _prepo;

        public CreateEventsController(IEventRepository erepo,
        IUserRepository urepo, IPaymentRepository prepo)
        {
            _prepo = prepo;
            _urepo = urepo;
            _erepo = erepo;
        }

        [HttpGet("bookingexists")]
        public async Task<IActionResult> BookingExists(int DjId, int EventId)
        {
            bool bookingExists = await _erepo.BookingExists(DjId, EventId);

            return Ok(bookingExists);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent(Event ev, int promoterId)
        {
            Event eventToCreate = new Event();

            eventToCreate.DateCreated = DateTime.Now;
            eventToCreate.DateTimeOfEvent = ev.DateTimeOfEvent;
            eventToCreate.LengthOfEvent = ev.LengthOfEvent;
            eventToCreate.TotalCost = ev.TotalCost;
            eventToCreate.EventAddress = ev.EventAddress;
            eventToCreate.Postcode = ev.Postcode;
            eventToCreate.EventStatus = true;
            eventToCreate.PromoterId = promoterId;

            _erepo.Add(eventToCreate);

            bool complete = await _erepo.SaveAll();

            return Ok(complete);
        }

    }
}