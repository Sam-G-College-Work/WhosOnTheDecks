using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using Stripe;
using System.Threading.Tasks;

namespace WhosOnTheDecks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IEventRepository _erepo;
        private readonly IPaymentRepository _prepo;

        public PaymentsController(IEventRepository erepo,
        IPaymentRepository prepo)
        {
            _prepo = prepo;
            _erepo = erepo;
        }

        // public async Task<IActionResult> Charge(string email, string token)
        // {
        //     CustomerService customerServices = new CustomerService();
        // }

    }
}