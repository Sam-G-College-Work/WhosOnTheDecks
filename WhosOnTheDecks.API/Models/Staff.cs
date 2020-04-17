using System;
using System.ComponentModel.DataAnnotations;

namespace WhosOnTheDecks.API.Models
{
    //This is the Staff class used to create a Staff object
    //The Staff is a user of the site and will complete administritive duties for the site
    //A staff has 2 roles Staff and Admin, Admin is a superuser that will have complete control
    //Date of birth defines the Staff object
    //As Staff inherits from user all user attributes will make up the Staff object too
    public class Staff : User
    {
        //DateTime DateOfBirth used to store the Staff members date of birth
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }
        
    }
}