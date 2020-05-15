using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhosOnTheDecks.API.Dtos
{
    //Payment to create Dto is used to verify the 3 required fields for payment
    //This will be used to complete the payment object in the database upon successful payment
    public class PaymentToCreateDto
    {
        [Required]
        [Display(Name = "Card Number")]
        [RegularExpression(@"^[0-9]{16}$",
         ErrorMessage = "Please enter a valid card number 16 digits long.")]
        public long CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiry Date")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/?([2]{1}[0]{1}[2-3]{1}[0-9]{1}|[2-3]{1}[0-9]{1})$",
         ErrorMessage = "Please enter a valid expiry in the format 01/21.")]
        public string ExpiryDate { get; set; }

        [Required]
        [Display(Name = "Security Code")]
        [RegularExpression(@"^[0-9]{3}$",
         ErrorMessage = "Please enter a valid security code 3 digits on the back of the card.")]
        public int SecurityCode { get; set; }

    }
}