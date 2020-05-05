using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhosOnTheDecks.API.Models
{
    //Payment class is used to create a payment object
    //A payment is created following an event being made 
    //The payment amount will be calculated as a reuslt of the bookings that make the event
    //And the hourlay rates of the DJs that make up the bookings
    public class Payment
    {
        //Primary key for clcass
        //Integer PaymentId is used to give each payment a unique ID
        [Key]
        public int PaymentId { get; set; }

        //Storing integer of DjId to finalise booking after event is paid for
        public int DjId { get; set; }

        //Navigational Property
        //A single payment will be tired to a single event
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}