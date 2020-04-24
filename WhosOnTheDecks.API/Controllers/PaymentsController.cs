using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;

namespace WhosOnTheDecks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepository _repo;


        public PaymentsController(IPaymentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _repo.GetPayments();

            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _repo.GetPayment(id);

            return Ok(payment);
        }
    }
}