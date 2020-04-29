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
        private readonly IEventRepository _repo;

        private List<EventDisplayDto> promoterEvents = new List<EventDisplayDto>();


        public EventsController(IEventRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("getevents/{id?}")]
        public async Task<IActionResult> GetEvents(int id)
        {
            var events = await _repo.GetEvents();

            foreach (Event ev in events)
            {
                if (ev.PromoterId == id)
                {
                    EventDisplayDto edto = new EventDisplayDto();
                    edto.EventId = ev.EventId;
                    edto.DateCreated = ev.DateCreated;
                    edto.DateOfEvent = ev.DateOfEvent;
                    edto.EventStartTime = ev.EventStartTime;
                    edto.EventEndTime = ev.EventEndTime;
                    edto.CostOfEvent = ev.CostOfEvent;
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
            var ev = await _repo.GetEvent(id);

            EventDisplayDto edto = new EventDisplayDto();
            edto.EventId = ev.EventId;
            edto.DateCreated = ev.DateCreated;
            edto.DateOfEvent = ev.DateOfEvent;
            edto.EventStartTime = ev.EventStartTime;
            edto.EventEndTime = ev.EventEndTime;
            edto.CostOfEvent = ev.CostOfEvent;
            edto.EventAddress = ev.EventAddress;
            edto.Postcode = ev.Postcode;

            return Ok(edto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent(EventToCreateDto eventDto, int promoterId)
        {
            Event eventToCreate = new Event();

            eventToCreate.DateCreated = DateTime.Now;
            eventToCreate.DateOfEvent = eventDto.DateOfEvent;
            eventToCreate.EventStartTime = eventDto.EventStartTime;
            eventToCreate.EventEndTime = eventDto.EventEndTime;
            eventToCreate.EventAddress = eventDto.EventAddress;
            eventToCreate.Postcode = eventDto.Postcode;
            eventToCreate.EventStatus = true;
            eventToCreate.PromoterId = promoterId;

            _repo.Add(eventToCreate);

            bool complete = await _repo.SaveAll();

            return Ok(complete);
        }

    }
}