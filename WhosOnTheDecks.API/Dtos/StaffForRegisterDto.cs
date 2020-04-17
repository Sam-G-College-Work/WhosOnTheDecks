using System;
using System.ComponentModel.DataAnnotations;

namespace WhosOnTheDecks.API.Dtos
{
    public class StaffForRegisterDto : UserForRegisterDto
    {
        //DateTime DateOfBirth used to store the Staff members date of birth
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }
    }
}