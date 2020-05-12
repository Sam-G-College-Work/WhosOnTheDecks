using System;
using System.ComponentModel.DataAnnotations;

namespace WhosOnTheDecks.API.Dtos
{
    //Payment to create Dto is used to verify the 3 required fields for payment
    //This will be used to complete the payment object in the database upon successful payment
    public class PaymentToCreateDto
    {
        [Required]
        [Display(Name = "Card Number")]
        [DataType(DataType.CreditCard)]
        public long CardNumber { get; set; }

        //Decimal Hourly Rate used to store DJs hourly cost
        [Required]
        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        //string Equipment used to store the DJs required equipment
        [Required]
        [Display(Name = "Security Code")]
        public int SecurityCode { get; set; }
    }
}