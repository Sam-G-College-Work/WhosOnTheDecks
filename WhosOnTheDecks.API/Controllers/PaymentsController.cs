using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using Stripe;
using System.Threading.Tasks;
using WhosOnTheDecks.API.Models;
using System;

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
        public async Task<IActionResult> Charge(int promoterId)
        {
            var promoter = await _urepo.GetPromoter(promoterId);

            var payments = await _prepo.GetPayments();

            decimal total = 0.00M;

            foreach (Payment payment in payments)
            {
                var ev = await _erepo.GetEvent(payment.EventId);

                if (ev.PromoterId == promoterId)
                {
                    total += ev.TotalCost;
                }
            }


            foreach (Payment payment in payments)
            {
                var ev = await _erepo.GetEvent(payment.EventId);

                if (ev.PromoterId == promoterId)
                {
                    Booking booking = new Booking();
                    booking.DjId = payment.DjId;
                    booking.EventId = payment.EventId;
                    booking.BookingStatus = BookingStatus.Awaiting;

                    _erepo.Add(booking);

                    await _erepo.SaveAll();

                    _prepo.Remove(payment);

                    await _prepo.SaveAll();
                }
            }

            return Ok("Payment was successful");
        }

    }
}