using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PilotsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CitiesService _cities;

        public PilotsController(ApplicationDbContext context, CitiesService cities)
        {
            _context = context;
            _cities = cities;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pilot>>> GetAll()
        {
            var pilots = await _context.Pilots
                .ToArrayAsync();

            if (pilots == null)
            {
                return NotFound();
            }

            foreach (var pilot in pilots)
            {
                try
                {
                    var city = await _cities.GetByIdAsync(pilot.CityId);
                    pilot.City = city;
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok(pilots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActionResult<Pilot>>> GetById(int id)
        {
            var pilot = await _context.Pilots
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (pilot == null)
            {
                return NotFound();
            }

            try
            {
                var city = await _cities.GetByIdAsync(pilot.CityId);
                pilot.City = city;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(pilot);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Pilot pilot)
        {
            if (pilot == null)
            {
                return NotFound();
            }

            try
            {
                var city = await _cities.GetByIdAsync(pilot.CityId);
                pilot.CityId = city.Id;
                pilot.City = null;
                //pilot.City.Country = null;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            _context.Add(pilot);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(new { message = "ID не найден" });
            }

            var pilot = await _context.Pilots
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (pilot == null)
            {
                return NotFound(new { message = "Пилот не найден!" });
            }

            _context.Pilots.Remove(pilot);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Pilot data)
        {
            if (id != data.Id)
            {
                return BadRequest();
            }

            _context.Entry(data).State = EntityState.Modified;

            try
            {
                try
                {
                    var city = await _cities.GetByIdAsync(data.CityId);
                    data.CityId = city.Id;
                    data.City.Country = null;
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Pilots.Any(c => c.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

    }
}
