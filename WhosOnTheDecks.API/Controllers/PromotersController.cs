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
        private readonly IUserCrudRepository _repo;


        public PromotersController(IUserCrudRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPromoters()
        {
            var promoters = await _repo.GetPromoters();

            return Ok(promoters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromoter(int id)
        {
            var promoter = await _repo.GetPromoter(id);

            return Ok(promoter);
        }
    }
}