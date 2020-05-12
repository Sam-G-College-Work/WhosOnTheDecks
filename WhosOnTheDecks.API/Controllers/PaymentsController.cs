using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using System.Threading.Tasks;
using WhosOnTheDecks.API.Models;
using WhosOnTheDecks.API.Dtos;

namespace WhosOnTheDecks.API.Controllers
{
    //Auth controller created to authorise users registering and logging in
    //Inherits from the controller Base as thats where the underlying methods for url requests are
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        //Property of IEventRepository is declared
        private readonly IEventRepository _erepo;

        //Property of IEventRepository is declared
        private readonly IPaymentRepository _prepo;

        //Property of IEventRepository is declared
        private readonly IUserRepository _urepo;

        //Constructor is used to insialise the repository properties from above
        public PaymentsController(IEventRepository erepo,
        IPaymentRepository prepo, IUserRepository urepo)
        {
            _urepo = urepo;
            _prepo = prepo;
            _erepo = erepo;
        }

        //Charge methos takes in card details in the form of a paymenttocreatedto and the prmoter id
        [HttpPost("payment/{promoterId}")]
        public async Task<IActionResult> Charge(int promoterId, PaymentToCreateDto cardDetails)
        {
            //Payments are pulled from the database
            var payments = await _prepo.GetPayments();

            //A loop is started to itterate throught he payments in payments
            foreach (Payment payment in payments)
            {
                //A check is perfomred to see if the payment has been revcieved
                if (payment.PaymentRecieved == false)
                {
                    //If false the event tied to the payment is pulled from the databse
                    var ev = await _erepo.GetEvent(payment.EventId);

                    //A check is perofrmed to see if the entered promoterId matches the event tied to the payments promoter id
                    if (ev.PromoterId == promoterId)
                    {
                        //A new booking is created so the dj can respond
                        Booking booking = new Booking();
                        booking.DjId = payment.DjId;
                        booking.EventId = payment.EventId;
                        booking.BookingStatus = BookingStatus.Awaiting;

                        //The payment is updated with the entered card details
                        payment.CardNumber = cardDetails.CardNumber;
                        payment.ExpiryDate = cardDetails.ExpiryDate;
                        payment.SecurityCode = cardDetails.SecurityCode;
                        payment.PaymentRecieved = true;

                        //The new booking is then added to the database
                        _erepo.Add(booking);

                        //The events database is then saved
                        await _erepo.SaveAll();

                        //The payment is then updated in the database
                        _prepo.Update(payment);

                        //Payments database is then saved
                        await _prepo.SaveAll();
                    }
                }
            }
            //A status of Ok is then returned
            return Ok();
        }
    }
}