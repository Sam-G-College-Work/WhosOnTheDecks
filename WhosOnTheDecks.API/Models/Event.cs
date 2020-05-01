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
        [Display(Name = "Date and Time of Event")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateTimeOfEvent { get; set; }

        //DateTime EventStartTime used to record the start time of the event
        [Required]
        [Display(Name = "Length of Event")]
        [DataType(DataType.Duration)]
        public int LengthOfEvent { get; set; }

        //Decimal CostOfEvent used to store the entry cost of the event
        [Required]
        [Display(Name = "Total Cost")]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }

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
        //Links Event to a single promoter
        //An event is created by one promoter
        [ForeignKey("Promoter")]
        public int PromoterId { get; set; }
        public Promoter Promoter { get; set; }

    }
}