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
    //CreateEvents controller created to create, edit and delete events. This iwll also be used to get avaliable djs on selected date
    //Inherits from the controller Base as thats where the underlying methods for url requests are
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreateEventsController : ControllerBase
    {
        //Property of IEventRepository is declared
        private readonly IEventRepository _erepo;

        //Property of IPaymentRepository is declared
        private readonly IPaymentRepository _prepo;

        //Property of IUserRepository is declared
        private readonly IUserRepository _urepo;

        //Constructor is used to insialise the repository properties from above
        public CreateEventsController(IEventRepository erepo,
        IUserRepository urepo, IPaymentRepository prepo)
        {
            _prepo = prepo;
            _urepo = urepo;
            _erepo = erepo;
        }

        //GetAvaliableDjs will take in an event object and get Dj's that have no active booking on the selected date
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

            //A loop is started to itterate through all payments in payments table
            foreach (Payment payment in payments)
            {
                //A check is performed to see if the payment has bee recived
                //If it has not the event is still to be paid for
                if (payment.PaymentRecieved == false)
                {
                    //The event is pulled from the databse that matches the vent Id related to the payment
                    var evToRemove = await _erepo.GetEvent(payment.EventId);

                    //As the event is still to be paid for it is removed from the event to check list
                    eventsToCheck.Remove(evToRemove);

                    //The dj related to the event is located from the db using the payment djid
                    var djToRemove = await _urepo.GetDj(payment.DjId);

                    //A check is perofrmed to see if the event in the basket that is still to be paid for 
                    //Is occuring on the same date ad the new event being created
                    //If so the dj will be removed as they are currently in the process of being booked
                    if (evToRemove.DateTimeOfEvent.Date == evNew.DateTimeOfEvent.Date)
                    {
                        AvaliableDjsToConvert.Remove(djToRemove);
                    }
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

            //A list of the DjToDisplayDto objects are returned with an Ok Status
            return Ok(AvaliableDjsToDisplay);
        }


        //CreateEvent is used to take in a created event object a promoterId and a DjId
        //The moethod will then convert the inputed info into a full event object and add this to the database
        [HttpPost("create/{promoterId}/{djId}")]
        public async Task<IActionResult> CreateEvent(int promoterId, int djId,
         Event ev)
        {
            //Event object is created
            Event eventToCreate = new Event();

            //Dj is located and pulled from databse using entered djid
            var dj = await _urepo.GetDj(djId);

            //EventToCreate objects fields are populated
            eventToCreate.DateCreated = DateTime.Now;
            eventToCreate.DateTimeOfEvent = ev.DateTimeOfEvent.AddHours(1);
            eventToCreate.LengthOfEvent = ev.LengthOfEvent;
            eventToCreate.TotalCost = (ev.LengthOfEvent * dj.HourlyRate);
            eventToCreate.EventAddress = ev.EventAddress;
            eventToCreate.Postcode = ev.Postcode;
            eventToCreate.EventStatus = true;
            eventToCreate.PromoterId = promoterId;

            //the event object is then added to the databse
            _erepo.Add(eventToCreate);

            //The events database is then saved
            await _erepo.SaveAll();

            //A new payment object is then created
            Payment paymentNew = new Payment();

            //Event Id is tied to payment
            paymentNew.EventId = eventToCreate.EventId;

            //Dj Id is tied to payment
            paymentNew.DjId = djId;

            //PaymentRecieved status is set to false
            paymentNew.PaymentRecieved = false;

            //Pending payment is then added to payments databse
            _prepo.Add(paymentNew);

            //Databse is then saved
            await _prepo.SaveAll();

            //A Ok status is then returned
            return Ok();
        }

        //Get orders will get all the active unpaid for events related to the promoterId
        [HttpGet("getorders/{promoterId}")]
        public async Task<IActionResult> getOrders(int promoterId)
        {
            //List of events is created
            List<Event> promotersEvents = new List<Event>();

            //Payments ar epulled from the database
            var payments = await _prepo.GetPayments();

            //A loop is started to itterate through all payments in payments
            foreach (Payment payment in payments)
            {
                //An inital check to see if the payment has been recieved is performed
                if (payment.PaymentRecieved == false)
                {
                    //If the payment is due the event related to the payment is pulled
                    //from the databse 
                    var ev = await _erepo.GetEvent(payment.EventId);

                    //A check is then performed to see if the event promoter id matches the entered promoterid
                    if (ev.PromoterId == promoterId)
                    {
                        //The event is then added to the promoters events
                        promotersEvents.Add(ev);
                    }
                }
            }
            //The promoter events list is returned with a status of ok
            return Ok(promotersEvents);
        }

        //Cancel will locate all unpad for events and payments in databse related to promoterId entered
        //The method will then remove these items and save the databse
        [HttpDelete("cancel/{promoterId}")]
        public async Task<IActionResult> Cancel(int promoterId)
        {
            //Payments are pulled from databse
            var payments = await _prepo.GetPayments();

            //A loop is started to itterate through all payments in payments
            foreach (Payment payment in payments)
            {
                //An inital check to see if the payment has been recieved
                if (payment.PaymentRecieved == false)
                {
                    //If the payment is still due to the related event is then located and pulled from the databse
                    var eventToCheck = await _erepo.GetEvent(payment.EventId);

                    //The event promoter id is then matched to the entered promoter id
                    if (eventToCheck.PromoterId == promoterId)
                    {
                        //If its a match the payment is removed
                        _prepo.Remove(payment);

                        //The payments database is then saved
                        await _prepo.SaveAll();

                        //If its a match the event is removed from the database
                        _erepo.Remove(eventToCheck);

                        //Teh events databse is then saved
                        await _erepo.SaveAll();
                    }
                }
            }

            //A status of ok is then returned
            return Ok();
        }

        //ShoppingExists takes in a promoterId and return a ture or false answer if hey have shopping that is unpaid for in the their basket
        [HttpGet("shoppingexists/{promoterId}")]
        public async Task<bool> ShoppingExists(int promoterId)
        {
            //Boolean is declared and set to false
            bool shoppingExists = false;

            //Payments are then pulled from the database
            var payments = await _prepo.GetPayments();

            //A loop is started to itterated through all payments in payments
            foreach (Payment payment in payments)
            {
                //A check is perofmred to see if the payment has been recieved
                if (payment.PaymentRecieved == false)
                {
                    //If not the event tied to the payment is pulled
                    var ev = await _erepo.GetEvent(payment.EventId);

                    //A check to ensure the event in the basket is tied to the promoter id enetered
                    if (ev.PromoterId == promoterId)
                    {
                        //If any event that is unpaid for exisists in the shopping basket tied to the promoter
                        //The boolean shopping exists will be set to true
                        shoppingExists = true;
                    }
                }

            }

            //the boolean is then returned.
            return shoppingExists;
        }

    }
}