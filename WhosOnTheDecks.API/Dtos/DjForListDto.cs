using System.ComponentModel.DataAnnotations;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Dtos
{
    public class DjForListDto
    {
        //string Name for DJ Name
        public string DjName { get; set; }

        //Decimal Hourly Rate used to store DJs hourly cost
        public decimal HourlyRate { get; set; }

        //string Equipment used to store the DJs required equipment
        public string Equipment { get; set; }

        //Enum Genre is used to confirmt he genre of music the DJ plays
        public Genre Genre { get; set; }
    }
}