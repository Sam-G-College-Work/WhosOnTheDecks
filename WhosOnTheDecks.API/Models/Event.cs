using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhosOnTheDecks.API.Models
{
    //Event class used to create event object
    //An event is created by a promoter and is made up of bookings
    public class Event
    {
        //Primary Key for table
        //Integer EventId used to store a unique Id
        [Key]
        public int EventId { get; set; }

        //DateTime DateCreated used to store the Date the event was made
        [Required]
        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        //Datetime DateOfEvent is used to store the Date of the Event
        [Required]
        [Display(Name = "Date of Event")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfEvent { get; set; }

        //DateTime EventStartTime used to record the start time of the event
        [Required]
        [Display(Name = "Event Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime EventStartTime { get; set; }

        //DateTime EventEndTime used to record the end time of the event
        [Required]
        [Display(Name = "Event End Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime EventEndTime { get; set; }

        //Decimal CostOfEvent used to store the entry cost of the event
        [Required]
        [Display(Name = "Entry Cost")]
        [DataType(DataType.Currency)]
        public decimal CostOfEvent { get; set; }

        //Boolean EventStatus used to show if Event is Active or Cancelled
        [Required]
        [Display(Name = "Event Status")]
        public bool EventStatus { get; set; }

        //String EventAddress is used to store the address the event takes place
        [Required]
        [Display(Name = "Event Address")]
        [DataType(DataType.MultilineText)]
        public string EventAddress { get; set; }

        //String Postcode is used to store the postcode of the event 
        [Required]
        [Display(Name = "Postcode")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }

        //Navigational Property
        //Links Event to a list of bookings
        //An event is made of one to many bookings
        [ForeignKey("Bookings")]
        public List<Booking> Bookings { get; set; }

        //Navigational Property
        //Links Event to a single promoter
        //An event is created by one promoter
        [ForeignKey("Promoter")]
        public int PromoterId { get; set; }
        public Promoter Promoter { get; set; }

        //Navigational Property
        //Links Event to a single payment
        //Every event will have a total cost calculated from the bookings and the DJ's rate
        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

    }
}