using System;

namespace WhosOnTheDecks.API.Dtos
{
    //EventToCreateDto exists to abstract a layer away from event model
    //This allows the front end to recieve in the required data from the dto
    //Which will then be displayed in the front end 
    public class EventDisplayDto
    {

        //Integer EventId used to store a unique Id
        public int EventId { get; set; }

        //DateTime DateCreated used to store the Date the event was made
        public DateTime DateCreated { get; set; }

        //Datetime DateTimeOfEvent is used to store the Date and Time of the Event
        public DateTime DateTimeOfEvent { get; set; }

        //int LengthOfEvent indicates how long the event will take place
        public int LengthOfEvent { get; set; }

        //decimal to show the total cost of the event
        public decimal TotalCost { get; set; }

        //Boolean EventStatus used to show if Event is Active or Cancelled
        public bool EventStatus { get; set; }

        //String EventAddress is used to store the address the event takes place
        public string EventAddress { get; set; }

        //String Postcode is used to store the postcode of the event 
        public string Postcode { get; set; }
    }
}