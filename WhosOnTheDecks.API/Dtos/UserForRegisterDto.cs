using System.ComponentModel.DataAnnotations;

namespace WhosOnTheDecks.API.Dtos
{
    //Data transfer object is used so the whole user object doen not need to be created 
    //This is due to the password being a string and at user level will be stored as a hash and salt
    //By using the Dto I can apply validation to make sure the user entered data is correct
    public class UserForRegisterDto
    {
        [Required]
        [EmailAddress(
            ErrorMessage = "You must enter a valid email address"
        )]
        public string Email { get; set; }

        [Required]
        [StringLength(8, MinimumLength =4, 
            ErrorMessage ="You must create a password between 4 and 8 characters long"
            )]
        public string Password { get; set; }
    }
}