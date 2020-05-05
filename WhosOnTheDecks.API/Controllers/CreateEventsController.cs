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
    public class CreateEventsController : ControllerBase
    {
        private readonly IEventRepository _erepo;
        private readonly IUserRepository _urepo;

        private Dictionary<int, Event> EventShoppingBasket = new Dictionary<int, Event>();

        private Dictionary<int, Booking> BookingShoppingBasket = new Dictionary<int, Booking>();

        private List<Dj> AvaliableDjsToConvert = new List<Dj>();

        private List<DjDisplayDto> AvaliableDjsToDisplay = new List<DjDisplayDto>();

        public CreateEventsController(IEventRepository erepo,
        IUserRepository urepo)
        {
            _urepo = urepo;
            _erepo = erepo;
        }

        [HttpGet("avaliabledjs")]
        public async Task<IActionResult> GetAvaliableDjs(Event evNew)
        {
            //List of all events currently in databse
            var allEvents = await _erepo.GetEvents();

            //List of all Djs in database
            var avaliableDjsToList = await _urepo.GetDjs();

            //List created to hold all bookings on the same date as the event to create
            List<Booking> bookingsToCheck = new List<Booking>();

            //A loop is performed to take all Djs and 
            //put them into a list of Dto display object
            foreach (Dj dj in avaliableDjsToList)
            {
                AvaliableDjsToConvert.Add(dj);
            }

            if (BookingShoppingBasket.Count > 0)
            {
                foreach (KeyValuePair<int, Booking> kvp in BookingShoppingBasket)
                {
                    Booking book = kvp.Value;

                    var Dj = await _urepo.GetDj(book.DjId);

                    AvaliableDjsToConvert.Remove(Dj);
                }
            }

            //Loop is started to itterate through each event in allEvents List
            foreach (Event ev in allEvents)
            {
                DateTime newDate = evNew.DateTimeOfEvent.Date;

                DateTime compareDate = ev.DateTimeOfEvent.Date;

                //A check is performed between the date and time of the new event and 
                if (newDate == compareDate)
                {

                    var booking = await _erepo.GetBooking(ev.EventId);

                    if (booking.BookingStatus == BookingStatus.Accepted
                    || booking.BookingStatus == BookingStatus.Awaiting)
                    {
                        var Dj = await _urepo.GetDj(booking.DjId);

                        AvaliableDjsToConvert.Remove(Dj);
                    }
                }
            }

            foreach (Dj dj in AvaliableDjsToConvert)
            {
                DjDisplayDto djdto = new DjDisplayDto();

                djdto.DjId = dj.Id;
                djdto.DjName = dj.DjName;
                djdto.Equipment = dj.Equipment;
                djdto.HourlyRate = dj.HourlyRate;
                djdto.Genre = dj.Genre.ToString();

                AvaliableDjsToDisplay.Add(djdto);
            }

            return Ok(AvaliableDjsToDisplay);
        }


        [HttpPost("create/{promoterId}/{djId}")]
        public async Task<IActionResult> CreateEvent(int promoterId, int djId,
         Event ev)
        {
            Event eventToCreate = new Event();

            Booking bookingToCreate = new Booking();

            var dj = await _urepo.GetDj(djId);

            eventToCreate.DateCreated = DateTime.Now;
            eventToCreate.DateTimeOfEvent = ev.DateTimeOfEvent;
            eventToCreate.LengthOfEvent = ev.LengthOfEvent;
            eventToCreate.TotalCost = (ev.LengthOfEvent * dj.HourlyRate);
            eventToCreate.EventAddress = ev.EventAddress;
            eventToCreate.Postcode = ev.Postcode;
            eventToCreate.EventStatus = true;
            eventToCreate.PromoterId = promoterId;

            EventShoppingBasket.Add(promoterId, eventToCreate);

            bookingToCreate.BookingStatus = BookingStatus.Awaiting;
            bookingToCreate.DjId = djId;

            BookingShoppingBasket.Add(promoterId, bookingToCreate);

            return Ok(EventShoppingBasket);
        }

        [HttpPost("cancel")]
        public IActionResult Cancel()
        {
            EventShoppingBasket.Clear();
            BookingShoppingBasket.Clear();

            return Ok("Your orders have been removed");
        }

        [HttpGet("listevents")]
        public IActionResult ListEvents()
        {
            return Ok(EventShoppingBasket);
        }
    }
}