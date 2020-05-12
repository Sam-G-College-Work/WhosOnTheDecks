using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using System.Threading.Tasks;
using WhosOnTheDecks.API.Models;
using WhosOnTheDecks.API.Dtos;

namespace WhosOnTheDecks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IEventRepository _erepo;
        private readonly IPaymentRepository _prepo;
        private readonly IUserRepository _urepo;

        public PaymentsController(IEventRepository erepo,
        IPaymentRepository prepo, IUserRepository urepo)
        {
            _urepo = urepo;
            _prepo = prepo;
            _erepo = erepo;
        }

        [HttpPost("payment/{promoterId}")]
        public async Task<IActionResult> Charge(int promoterId, PaymentToCreateDto cardDetails)
        {
            var payments = await _prepo.GetPayments();

            foreach (Payment payment in payments)
            {
                if (payment.PaymentRecieved == false)
                {
                    var ev = await _erepo.GetEvent(payment.EventId);

                    if (ev.PromoterId == promoterId)
                    {
                        Booking booking = new Booking();
                        booking.DjId = payment.DjId;
                        booking.EventId = payment.EventId;
                        booking.BookingStatus = BookingStatus.Awaiting;

                        payment.CardNumber = cardDetails.CardNumber;
                        payment.ExpiryDate = cardDetails.ExpiryDate;
                        payment.SecurityCode = cardDetails.SecurityCode;
                        payment.PaymentRecieved = true;

                        _erepo.Add(booking);

                        await _erepo.SaveAll();

                        _prepo.Update(payment);

                        await _prepo.SaveAll();
                    }
                }
            }
            return Ok();
        }
    }
}