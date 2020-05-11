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
            CustomerService customerServices = new CustomerService();

            ChargeService chargeservice = new ChargeService();

            var promoter = await _urepo.GetPromoter(promoterId);

            var payments = await _prepo.GetPayments();

            decimal total = 0.00M;

            var options = new TokenCreateOptions
            {
                Card = new CreditCardOptions
                {
                    Number = "4242424242424242",
                    ExpYear = 2021,
                    ExpMonth = 5,
                    Cvc = "123"
                }
            };

            var service = new TokenService();

            Token stripeToken = service.Create(options);

            foreach (Payment payment in payments)
            {
                var ev = await _erepo.GetEvent(payment.EventId);

                if (ev.PromoterId == promoterId)
                {
                    total += ev.TotalCost;
                }
            }

            var customer = customerServices.Create(new CustomerCreateOptions
            {
                Email = promoter.Email,
                Source = stripeToken.ToString()
            });

            var charge = chargeservice.Create(new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(Convert.ToInt32(total * 100)),
                Description = "A test Charge",
                Currency = "GBP",
                Customer = customer.Id
            });

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