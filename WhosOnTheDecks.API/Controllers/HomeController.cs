using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using WhosOnTheDecks.API.Dtos;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Controllers
{
    //Auth controller created to authorise users registering and logging in
    //Inherits from the controller Base as thats where the underlying methods for url requests are
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        //Property of IEventRepository is declared
        private readonly IUserRepository _urepo;

        //Constructor is used to insialise the repository properties from above
        public HomeController(IUserRepository urepo)
        {
            _urepo = urepo;
        }

        //GetDjs method is used to get all djs in database and retunr them to the front end to display
        [HttpGet]
        public async Task<IActionResult> GetDjs()
        {
            //Djs are pulled from the database 
            var djsList = await _urepo.GetDjs();

            //DjDisplayDto object is then created to send to the front end
            List<DjDisplayDto> DjDtos = new List<DjDisplayDto>();

            //a loop is started to itterate through all djs in list
            foreach (Dj dj in djsList)
            {
                //A dj display dto obejct is then created
                //This will only send the information we want to the fron end
                DjDisplayDto djdto = new DjDisplayDto();

                djdto.DjId = dj.Id;
                djdto.DjName = dj.DjName;
                djdto.Equipment = dj.Equipment;
                djdto.HourlyRate = dj.HourlyRate;
                djdto.Genre = dj.Genre.ToString();

                DjDtos.Add(djdto);
            }

            //List of djdtos is then returned with a staus of ok
            return Ok(DjDtos);
        }
    }
}