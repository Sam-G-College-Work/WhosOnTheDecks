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

        private List<Dj> AvaliableDjsToConvert = new List<Dj>();

        private List<DjDisplayDto> AvaliableDjsToDisplay = new List<DjDisplayDto>();

        public CreateEventsController(IEventRepository erepo,
        IUserRepository urepo, IPaymentRepository prepo)
        {
            _prepo = prepo;
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

            var payments = await _prepo.GetPayments();

            foreach (Payment payment in payments)
            {
                var dj = await _urepo.GetDj(payment.DjId);

                AvaliableDjsToConvert.Remove(dj);

                //Loop is started to itterate through each event in allEvents List
                foreach (Event ev in allEvents)
                {
                    if (payment.EventId != ev.EventId)
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

            return Ok(shoppingBasket);
        }

        [HttpGet("cancel/{promoterId}")]
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

            return Ok("Your orders have been removed");
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
    }
}