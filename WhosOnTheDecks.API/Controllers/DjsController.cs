using System;
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

        private List<DjDisplayDto> DjDtos = new List<DjDisplayDto>();

        public DjsController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDjs()
        {
            var djsList = await _repo.GetDjs();

            foreach (Dj dj in djsList)
            {
                DjDisplayDto djdto = new DjDisplayDto();

                djdto.DjId = dj.Id;
                djdto.DjName = dj.DjName;
                djdto.Equipment = dj.Equipment;
                djdto.HourlyRate = dj.HourlyRate;
                djdto.Genre = dj.Genre.ToString();

                this.DjDtos.Add(djdto);
            }

            return Ok(DjDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDj(int id)
        {
            var dj = await _repo.GetDj(id);

            DjDisplayDto djdto = new DjDisplayDto();

            djdto.DjId = dj.Id;
            djdto.DjName = dj.DjName;
            djdto.Equipment = dj.Equipment;
            djdto.HourlyRate = dj.HourlyRate;
            djdto.Genre = dj.Genre.ToString();

            return Ok(djdto);
        }
    }
}