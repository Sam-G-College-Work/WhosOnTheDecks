using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhosOnTheDecks.API.Models
{
    //This is the Promoter class used to create a Promoter object
    //The Promoter is a user of the site and will be the customer of the site
    //CompanyName defines the Promtoer object
    //A list of events links Promoter to Event
    //As Promoter inherits from User all user attributes will make up the Promoter object too
    public class Promoter : User
    {
        //String CompanyName is used to store the Company the Promoter is a part of
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        //Navigational Property
        //Links a payment to a single event
        [ForeignKey("Event")]
        public List<Event> Events { get; set; }

    }
}