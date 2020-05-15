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

        //An integer of DjId to finalise booking after event is paid for
        public int DjId { get; set; }

        //A long of CardNumber is used to store the cusotmers card number
        public long CardNumber { get; set; }

        //A string Expiry date is used to store the cards expiry date
        public string ExpiryDate { get; set; }

        //An Integer Security code is used to store the security code
        public int SecurityCode { get; set; }

        //A Boolean PaymentRecieved is used to determin weither the payment is pending
        public bool PaymentRecieved { get; set; }

        //Navigational Property
        //A single payment will be tired to a single event
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}