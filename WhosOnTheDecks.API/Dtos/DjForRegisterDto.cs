using System.ComponentModel.DataAnnotations;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Dtos
{
    //DJ for register Dto acts as an abstracted layer from DJ model
    //This allows the user to input the required data which 
    //Will be verified and then converted into a dj object to store
    //In th the auth controller
    public class DjForRegisterDto : UserForRegisterDto
    {
        [Required]
        [Display(Name = "DJ Name")]
        public string DjName { get; set; }

        //Decimal Hourly Rate used to store DJs hourly cost
        [Required]
        [Display(Name = "Hourly Rate")]
        [DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }

        //string Equipment used to store the DJs required equipment
        [Required]
        [Display(Name = "Equipment")]
        [DataType(DataType.MultilineText)]
        public string Equipment { get; set; }

        //Enum Genre is used to confirmt he genre of music the DJ plays
        [Required]
        [Display(Name = "Genre")]
        [EnumDataType(typeof(Genre))]
        public Genre Genre { get; set; }
    }
}