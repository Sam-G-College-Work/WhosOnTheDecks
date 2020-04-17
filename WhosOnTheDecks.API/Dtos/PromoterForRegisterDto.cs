using System.ComponentModel.DataAnnotations;

namespace WhosOnTheDecks.API.Dtos
{
    public class PromoterForRegisterDto : UserForRegisterDto
    {
         //String CompanyName is used to store the Company the Promoter is a part of
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

    }
}