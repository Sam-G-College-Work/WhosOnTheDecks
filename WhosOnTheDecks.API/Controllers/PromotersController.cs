using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WhosOnTheDecks.API.Data;

namespace WhosOnTheDecks.API.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotersController : ControllerBase
    {
        private readonly IUserRepository _repo;


        public PromotersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromoter(int id)
        {
            var promoter = await _repo.GetPromoter(id);

            return Ok(promoter);
        }
    }
}