using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaneTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlaneTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaneType>>> GetAll()
        {
            var result = await _context.PlaneTypes.ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaneType>> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.PlaneTypes.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PlaneType type)
        {
            _context.Add(type);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Ошибка при сохранении данных." });
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(new { message = "Тип самолета не найден" });
            }

            var planeType = await _context.PlaneTypes.FindAsync(id);

            if (planeType == null)
            {
                return BadRequest();
            }

            _context.PlaneTypes.Remove(planeType);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Тип самолета {planeType.Name} удален!" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] PlaneType data)
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
                if (_context.Planes.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = $"Тип самолета {oldName} изменен на {data.Name} {DateTime.Now}" });
        }
    }
}
