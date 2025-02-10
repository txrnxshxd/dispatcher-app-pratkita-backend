using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAll()
        {
            var result = await _context.Companies.ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var result = await _context.Companies.FindAsync(id);

            if (result == null)
            {
                return NotFound(new { message = "Не найдено!"} );
            }

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Company>> GetByName([FromQuery] string name)
        {
            var result = await _context.Companies.FirstOrDefaultAsync(n => n.Name.ToLower().Contains(name.ToLower()));

            if (result == null)
            {
                return NotFound(new { message = "Не найдено!" });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Company data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound(new { message = "Компания не найдена!"});
            }

            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return BadRequest();
            }

            _context.Companies.Remove(company);

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Компания {company.Name} успешно удалена!" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Company data)
        {
            if (id != data.Id)
            {
                return BadRequest();
            }

            string oldName = data.Name;

            _context.Entry(data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Companies.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = $"Компания {oldName} изменена на {data.Name} {DateTime.Now}"});
        }
    }
}
