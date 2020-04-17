using System.ComponentModel.DataAnnotations;

namespace WhosOnTheDecks.API.Models
{
    //The User class will be used to create User objects
    //The user class will be the parent class of all users for the site
    //DJ, Promoter and Staff will be child classes
    //The user class will be used to store all data relating to all users
    //Storing of a hashed password and salt for the hash will be used to increase site security
    public class User
    {
        //Primary Key for the user class
        //Integer Id used to store a unique Id for each user
        [Key]
        public int Id { get; set; }

        //String email used to store the users email
        //This field will primarily be used as the unique identifier for user accounts
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    
        //Byte Array PasswordHash used to store a hash of the users password
        [Required]
        public byte[] PasswordHash { get; set; }

        //Byte Array PasswordSalt is used to store a salt and key reference for the above hash
        [Required]
        public byte[] PasswordSalt {get; set;}

        //Boolean LockAccount used to indicate weither user can access their account
        [Display(Name = "Account Locked")]
        public bool LockAccount { get; set; }

        //String FirstName used to store the users first name
        [Required]
        [Display(Name =" First Name")]
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
        [Display(Name = "Phone Number")]
        public int PhoneNumber  { get; set; }

        //Enum Role used to store the users role
        [Display(Name = "Role")]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    }

    //Role enum to indicate the type of users and to allows certain privileges
    public enum Role 
    {
        Staff,
        Admin,
        Dj,
        Promoter
    }
}