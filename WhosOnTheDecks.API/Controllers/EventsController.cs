using System;
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


        public EventsController(IEventRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvent()
        {
            var events = await _repo.GetEvents();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var ev = await _repo.GetEvent(id);

            return Ok(ev);
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