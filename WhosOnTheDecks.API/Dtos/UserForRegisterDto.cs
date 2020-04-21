using System.ComponentModel.DataAnnotations;
using WhosOnTheDecks.API.Models;

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
        [StringLength(8, MinimumLength = 4,
            ErrorMessage = "You must create a password between 4 and 8 characters long"
            )]
        public string Password { get; set; }

        [Display(Name = "Account Locked")]
        public bool LockAccount { get; set; }

        //String FirstName used to store the users first name
        [Required]
        [Display(Name = " First Name")]
        public string FirstName { get; set; }

        //String LastName used to store the users last name
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //Integer HouseNumber used to store the users house number
        [Required]
        [Display(Name = "House Number")]
        public int HouseNumber { get; set; }

        //String StreetName used to store the users street name
        [Required]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        //String Postcode used to store the users postcode
        [Required]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        //Integer PhoneNumber used to store the users phone number 
        [Required]
        [DataType(DataType.PhoneNumber,
            ErrorMessage = "You must enter a valid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        //Enum Role used to store the users role
        [Display(Name = "Role")]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    }

}