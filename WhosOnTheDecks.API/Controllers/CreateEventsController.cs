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
        private readonly IPaymentRepository _prepo;
        private readonly IUserRepository _urepo;

        public CreateEventsController(IEventRepository erepo,
        IUserRepository urepo, IPaymentRepository prepo)
        {
            _prepo = prepo;
            _urepo = urepo;
            _erepo = erepo;
        }

        [HttpPost("avaliabledjs")]
        public async Task<IActionResult> GetAvaliableDjs(Event evNew)
        {
            //List of all events currently in databse
            var allEvents = await _erepo.GetEvents();

            //List of all Djs in database
            var allDjsToList = await _urepo.GetDjs();

            //List of all payments currently in shopping basket
            var payments = await _prepo.GetPayments();

            //List created to hold all bookings on the same date as the event to create
            List<Booking> bookingsToCheck = new List<Booking>();

            //A list of type dj is created to store all dj in db to an editable list
            List<Dj> AvaliableDjsToConvert = new List<Dj>();

            //A list of type DjDisplaySto is used to convert the list of avaliabel djs
            //into a display object we can safley share with the front end
            List<DjDisplayDto> AvaliableDjsToDisplay = new List<DjDisplayDto>();

            //A list of events is created to store the events from the database
            //This will allow the list to be edited without effecting the database
            List<Event> eventsToCheck = new List<Event>();

            //A loop is performed to take all Djs from DB and 
            //put them into a list of DJ object to be edited
            foreach (Dj dj in allDjsToList)
            {
                AvaliableDjsToConvert.Add(dj);
            }

            //A loop is performed to take all Events from DB and 
            //put them into a list of event object to be edited
            foreach (Event ev in allEvents)
            {
                eventsToCheck.Add(ev);
            }

            foreach (Payment payment in payments)
            {
                var evToRemove = await _erepo.GetEvent(payment.EventId);

                eventsToCheck.Remove(evToRemove);

                var djToRemove = await _urepo.GetDj(payment.DjId);

                if (evToRemove.DateTimeOfEvent.Date == evNew.DateTimeOfEvent.Date)
                {
                    AvaliableDjsToConvert.Remove(djToRemove);
                }

            }

            //Loop is started to itterate through each event in allEvents List
            foreach (Event ev in eventsToCheck)
            {
                //Datetime of new event is taken so only the date will be compared
                DateTime newDate = evNew.DateTimeOfEvent.Date;

                //Datetime of event in datatbase is taken so only the date will be compared
                DateTime compareDate = ev.DateTimeOfEvent.Date;

                //A check is performed between the date and time of the new event and 
                if (newDate == compareDate)
                {
                    //The booking of the event on the same date is found
                    var booking = await _erepo.GetBooking(ev.EventId);

                    //The booking is then checked to see if it has been accepted 
                    //or is awaiting response either will remove the dj from avaliability
                    if (booking.BookingStatus == BookingStatus.Accepted
                    || booking.BookingStatus == BookingStatus.Awaiting)
                    {
                        var Dj = await _urepo.GetDj(booking.DjId);

                        AvaliableDjsToConvert.Remove(Dj);
                    }


                }

            }

            //A final loop is performed to convert all dj objects to a dJ display object
            //This remvoes the djs password and sensitive information from being sent to the front end
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

            _erepo.Add(eventToCreate);

            await _erepo.SaveAll();

            Payment paymentNew = new Payment();

            paymentNew.EventId = eventToCreate.EventId;

            paymentNew.DjId = djId;

            _prepo.Add(paymentNew);

            await _prepo.SaveAll();

            List<Event> shoppingBasket = new List<Event>();

            var payments = await _prepo.GetPayments();

            foreach (Payment payment in payments)
            {
                var eventToCheck = await _erepo.GetEvent(payment.EventId);

                if (eventToCheck.PromoterId == promoterId)
                {
                    shoppingBasket.Add(eventToCheck);
                }
            }

            return Ok();
        }

        [HttpGet("getorders/{promoterId}")]
        public async Task<IActionResult> getOrders(int promoterId) {
            List<Event> promotersEvents = new List<Event>();

            var payments = await _prepo.GetPayments();
             
            foreach (Payment payment in payments)
            {
                var ev = await _erepo.GetEvent(payment.EventId);

                if (ev.PromoterId == promoterId)
                {
                    promotersEvents.Add(ev);
                }
            }

            return Ok(promotersEvents);
        }

        [HttpDelete("cancel/{promoterId}")]
        public async Task<IActionResult> Cancel(int promoterId)
        {
            var payments = await _prepo.GetPayments();

            foreach (Payment payment in payments)
            {
                var eventToCheck = await _erepo.GetEvent(payment.EventId);

                if (eventToCheck.PromoterId == promoterId)
                {
                    _prepo.Remove(payment);
                    await _prepo.SaveAll();
                    _erepo.Remove(eventToCheck);
                    await _erepo.SaveAll();
                }
            }

            return Ok();
        }

        [HttpGet("shoppingexists/{promoterId}")]
        public async Task<bool> ShoppingExists(int promoterId)
        {
            bool shoppingExists = false;

            var payments = await _prepo.GetPayments();

            foreach (Payment payment in payments)
            {
                var ev = await _erepo.GetEvent(payment.EventId);

                if (ev.PromoterId == promoterId)
                {
                    shoppingExists = true;
                }
            }

            return shoppingExists;
        }

        [HttpGet("gettotal/{id}")]
        public async Task<decimal> getTotal(int id)
        {
            decimal total = 0;

            var payments = await _prepo.GetPayments();

            foreach (Payment payment in payments)
            {
                var ev = await _erepo.GetEvent(payment.EventId);

                if (ev.PromoterId == id)
                {
                    total += ev.TotalCost;
                }
            }

            return total;
        }
    }
}