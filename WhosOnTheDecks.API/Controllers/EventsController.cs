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

        private List<Event> promoterEvents = new List<Event>();

        public EventsController(IEventRepository erepo)
        {
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
                    promoterEvents.Add(ev);
                }
            }


            return Ok(promoterEvents);
        }

        [HttpGet("getevent/{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var ev = await _eventRepo.GetEvent(id);

            return Ok(ev);
        }

    }
}