using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;

namespace WhosOnTheDecks.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DjsController : ControllerBase
    {
        private readonly IUserRepository _repo;


        public DjsController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDjs()
        {
            var djs = await _repo.GetDjs();

            return Ok(djs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDj(int id)
        {
            var dj = await _repo.GetDj(id);

            return Ok(dj);
        }
    }
}