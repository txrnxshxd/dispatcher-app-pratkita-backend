using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlanesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Plane data)
        {

            var existingCompany = await _context.Companies.FindAsync(data.CompanyId);

            if (existingCompany == null)
            {
                return NotFound(new { message = "Компания не найдена." });
            }

            var existingPlaneType = await _context.PlaneTypes.FindAsync(data.PlaneTypeId);

            if (existingPlaneType == null)
            {
                return NotFound(new { message = "Тип самолета не найден." });
            }

            var newPlane = new Plane
            {
                CompanyId = data.CompanyId,
                Company = existingCompany,
                Name = data.Name,
                TailNumber = data.TailNumber,
                PlaneTypeId = data.PlaneTypeId,
                PlaneType = existingPlaneType,
                PassengerCapacity = data.PassengerCapacity,
                ManufactureYear = data.ManufactureYear,
                LastCheckDate = data.LastCheckDate,
                TakeoffSpeed = data.TakeoffSpeed,
                CruisingSpeed = data.CruisingSpeed,
                LandingSpeed = data.LandingSpeed,
                MaxAltitude = data.MaxAltitude,
                MaxSpeed = data.MaxSpeed,
            };

            _context.Planes.Add(newPlane);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = newPlane.Id }, newPlane);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "Ошибка при сохранении данных.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plane>> GetById(int id)
        {
            var plane = await _context.Planes
                .Include(c => c.Company)
                .Include(t => t.PlaneType)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (plane == null)
            {
                return NotFound();
            }
            return Ok(plane);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plane>>> GetAll()
        {
            var planes = await _context.Planes
                .Include(c => c.Company)
                .Include(t => t.PlaneType)
                .ToListAsync();
            


            if (planes == null)
            {
                return NotFound();
            }
            return Ok(planes);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Plane>> GetByName([FromQuery] string name)
        {
            var result = await _context.Planes
                .Include(c => c.Company)
                .Include(t => t.PlaneType)
                .Where(n => n.Name.ToLower().Contains(name.ToLower()))
                .ToArrayAsync();

            if (result == null)
            {
                return NotFound(new { message = "Не найдено!" });
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(new { message = "ID не найден" });
            }

            var plane = await _context.Planes
                .Include(c => c.Company)
                .Include(t => t.PlaneType)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (plane == null)
            {
                return NotFound(new { message = "Самолет не найден!"} );
            }

            _context.Planes.Remove(plane);
            await _context.SaveChangesAsync();

            return Ok(
                new { 
                type = plane.PlaneType.Name,
                company = plane.Company.Name,
                name = plane.Name,
                tailNumber = plane.TailNumber,
                message = $"{plane.PlaneType.Name} самолет {plane.Company.Name} {plane.Name}, Бортовой номер: {plane.TailNumber} удален!" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Plane data)
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

            return Ok(new { message = $"Самолет {oldName} изменен на {data.Name} {DateTime.Now}" });
        }
    }
}
