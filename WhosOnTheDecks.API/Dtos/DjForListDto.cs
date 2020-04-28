using System.ComponentModel.DataAnnotations;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Dtos
{
    //DjForListDto act as a layer of abstraction from Dj model
    //This Dto is used to display Dj data and therefore does not need data annotations
    //This allows the back end to formulate the desired Dj object to display
    //Maintaining security over the Dj users account
    public class DjForListDto
    {
        //integer for Id
        public int DjId { get; set; }

        //string Name for DJ Name
        public string DjName { get; set; }

        //Decimal Hourly Rate used to store DJs hourly cost
        public decimal HourlyRate { get; set; }

        //string Equipment used to store the DJs required equipment
        public string Equipment { get; set; }

        //Enum Genre is used to confirmt he genre of music the DJ plays
        public string Genre { get; set; }
    }
}