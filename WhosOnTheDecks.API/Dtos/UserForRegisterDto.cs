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
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Please enter a valid first name. Numbers and special characters are not allowed.")]
        public string FirstName { get; set; }

        //String LastName used to store the users last name
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Please enter a valid last name. Numbers and special characters are not allowed.")]
        public string LastName { get; set; }

        //Integer HouseNumber used to store the users house number
        [Required]
        [Display(Name = "House Name or Number")]
        public string HouseNameOrNumber { get; set; }

        //String StreetName used to store the users street name
        [Required]
        [Display(Name = "Street Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Please enter a street name. Characters are not allowed.")]
        public string StreetName { get; set; }

        //String Postcode used to store the users postcode
        [Required]
        [Display(Name = "Postcode")]
        [RegularExpression(@"^(([gG][iI][rR] {0,}0[aA]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))$",
         ErrorMessage = "Please enter a Postcode eg. GA16 6TH.")]
        public string Postcode { get; set; }

        //Integer PhoneNumber used to store the users phone number 
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^((\\+44-?)|0)?[0-9]{11}$",
         ErrorMessage = "Please enter a valid phone number eg 07867879789")]
        public string PhoneNumber { get; set; }

        //Enum Role used to store the users role
        [Display(Name = "Role")]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    }

}