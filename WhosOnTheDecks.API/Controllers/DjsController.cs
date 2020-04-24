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
    public class DjsController : ControllerBase
    {
        private readonly IUserRepository _repo;

        private readonly List<DjForListDto> DjDtos;


        public DjsController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDjs()
        {
            var djs = await _repo.GetDjs();

            var djdto = new DjForListDto();

            List<DjForListDto> djdtos;

            foreach (Dj dj in djs)
            {
                djdto.DjName = dj.DjName;
                djdto.Equipment = dj.Equipment;
                djdto.HourlyRate = dj.HourlyRate;
                djdto.Genre = dj.Genre;

                djdtos.Add(djdto);
            }
            return Ok(djdtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDj(int id)
        {
            var dj = await _repo.GetDj(id);

            return Ok(dj);
        }
    }
}