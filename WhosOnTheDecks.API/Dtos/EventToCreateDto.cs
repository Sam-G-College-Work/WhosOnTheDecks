using System;
using System.ComponentModel.DataAnnotations;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Dtos
{
    //EventToCreateDto exists to abstract a layer away from event model
    //This allows the front end to send in the required data for the DTO
    //Which will then be verified and then added to the EventContext as a new event
    public class EventToCreateDto
    {

        //Datetime DateOfEvent is used to store the Date of the Event
        [Required]
        [Display(Name = "Date of Event")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd:MM:YYYY}")]
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

    }
}