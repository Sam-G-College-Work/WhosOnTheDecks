using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;

namespace WhosOnTheDecks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NewEventsController : ControllerBase
    {
        private readonly IEventRepository _erepo;
        private readonly IUserRepository _urepo;
        private readonly IPaymentRepository _prepo;

        public NewEventsController(IEventRepository erepo,
        IUserRepository urepo, IPaymentRepository prepo)
        {
            _prepo = prepo;
            _urepo = urepo;
            _erepo = erepo;
        }

        [HttpGet("bookingexists")]
        public async Task<IActionResult> BookingExists(int DjId, int EventId)
        {
            bool bookingExists = await _erepo.BookingExists(DjId, EventId);

            return Ok(bookingExists);
        }
    }
}