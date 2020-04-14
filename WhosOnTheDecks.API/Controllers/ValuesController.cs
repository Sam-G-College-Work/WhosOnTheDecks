using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhosOnTheDecks.API.Data;

namespace WhosOnTheDecks.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // Private readonly attribute added so class can access the database
        private readonly DataContext _context;

        //Constructor that sets _context to be incoming DataContext
        public ValuesController(DataContext context)
        {
            _context = context;
        }

        //URL connection to back end
        // http:localhost:5000/api/values
        // GET api/values
        //Using IActionResult so we get an ok response via URL is the get request succeeds
        // Method is asyncronous so it remains open whilst in use
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();

            return Ok(values);
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValues(int id)
        {
            //Use first or defualt to stop the application from throwing an exception if the reult is not there
            // Instead the application will return the default value which is null
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}