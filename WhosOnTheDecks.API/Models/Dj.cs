using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhosOnTheDecks.API.Models
{
    //This is the DJ class used to create a DJ object
    //The DJ is a user of the site but also the stock on the site
    //Name, Hourly rate, Equipment and genre define the DJ
    //A list of bookigns links DJ to Bookings
    //As DJ inherits from User all user attributes will make up the DJ object too
    public class Dj : User
    {
        //string Name for DJ Name
        [Required]
        [Display(Name = "DJ Name")]
        public string DjName { get; set; }

        //Decimal Hourly Rate used to store DJs hourly cost
        [Required]
        [Display(Name ="Hourly Rate")]
        [DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }

        //string Equipment used to store the DJs required equipment
        [Required]
        [Display(Name ="Equipment")]
        [DataType(DataType.MultilineText)]
        public string Equipment { get; set; }

        //Enum Genre is used to confirmt he genre of music the DJ plays
        [Required]
        [Display(Name="Genre")]
        [EnumDataType(typeof(Genre))]
        public Genre Genre { get; set; }

        //Navigational Property
        //Used to store a list of bookings a DJ has
        [ForeignKey("Booking")]
        public List<Booking> Bookings { get; set; }
    }

    //Enum created to store genres of music
    public enum Genre 
    {
        House,
        Techno,
        DnB,
        EDM,
        HappyHarcore,
        Jungle,
        DubStep,
        Electro
    }
}