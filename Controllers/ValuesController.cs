using DatingApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> getValue()
        {
            var value = await _context.values.ToListAsync();
            return Ok(value);
        }


        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<IActionResult> get(int id)
        {
                var value = await _context.values.FirstOrDefaultAsync(i => i.Id == id);
                return Ok(value);             

        }
    }
}
