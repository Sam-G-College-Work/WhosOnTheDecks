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

        //Decimal PaymentAmount will be the final amount payable by the Promoter
        [Required]
        [Display(Name = "Payment Amount")]
        [DataType(DataType.Currency)]
        public decimal PaymentAmount { get; set; }

        //Boolean PaymentStatus shows weither the payment succeded or failed
        [Required]
        [Display(Name = "Payment Status")]
        public bool PaymentStatus { get; set; }

        //Navigational Property
        //A single payment will be tired to a single event
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}