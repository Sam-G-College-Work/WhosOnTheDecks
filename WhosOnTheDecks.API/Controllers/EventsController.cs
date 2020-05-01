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
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepo;

        private readonly IPaymentRepository _paymentRepo;

        private List<EventDisplayDto> promoterEvents = new List<EventDisplayDto>();

        public EventsController(IEventRepository erepo, IPaymentRepository prepo)
        {
            _paymentRepo = prepo;
            _eventRepo = erepo;
        }

        [HttpGet("getevents/{id?}")]
        public async Task<IActionResult> GetEvents(int id)
        {
            var events = await _eventRepo.GetEvents();

            foreach (Event ev in events)
            {
                if (ev.PromoterId == id)
                {
                    var payment = await _paymentRepo.GetPayment(ev.EventId);

                    EventDisplayDto edto = new EventDisplayDto();
                    edto.EventId = ev.EventId;
                    edto.DateCreated = ev.DateCreated;
                    edto.DateTimeOfEvent = ev.DateTimeOfEvent;
                    edto.LengthOfEvent = ev.LengthOfEvent;
                    edto.TotalCost = ev.TotalCost;
                    edto.EventStatus = ev.EventStatus;
                    edto.EventAddress = ev.EventAddress;
                    edto.Postcode = ev.Postcode;

                    promoterEvents.Add(edto);
                }
            }


            return Ok(promoterEvents);
        }

        [HttpGet("getevent/{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var ev = await _eventRepo.GetEvent(id);

            var payment = await _paymentRepo.GetPayment(ev.EventId);

            EventDisplayDto edto = new EventDisplayDto();
            edto.EventId = ev.EventId;
            edto.DateCreated = ev.DateCreated;
            edto.DateTimeOfEvent = ev.DateTimeOfEvent;
            edto.LengthOfEvent = ev.LengthOfEvent;
            edto.TotalCost = ev.TotalCost;
            edto.EventStatus = ev.EventStatus;
            edto.EventAddress = ev.EventAddress;
            edto.Postcode = ev.Postcode;

            return Ok(edto);
        }

    }
}