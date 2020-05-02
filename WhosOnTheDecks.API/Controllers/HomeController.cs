using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;
using WhosOnTheDecks.API.Dtos;
using WhosOnTheDecks.API.Models;

namespace WhosOnTheDecks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserRepository _urepo;

        public HomeController(IUserRepository urepo)
        {
            _urepo = urepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDjs()
        {
            var djsList = await _urepo.GetDjs();

            List<DjDisplayDto> DjDtos = new List<DjDisplayDto>();

            foreach (Dj dj in djsList)
            {
                DjDisplayDto djdto = new DjDisplayDto();

                djdto.DjId = dj.Id;
                djdto.DjName = dj.DjName;
                djdto.Equipment = dj.Equipment;
                djdto.HourlyRate = dj.HourlyRate;
                djdto.Genre = dj.Genre.ToString();

                DjDtos.Add(djdto);
            }

            return Ok(DjDtos);
        }
    }
}