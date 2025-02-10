using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrierCompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CountriesService _countries;

        public CarrierCompaniesController(ApplicationDbContext context, CountriesService countries)
        {
            _context = context;
            _countries = countries;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrierCompany>>> GetAll()
        {
            var cities = await _context.CarrierCompanies
                .ToListAsync();

            if (cities == null)
            {
                return NotFound();
            }

            foreach (var city in cities)
            {
                try
                {
                    var country = await _countries.GetByIdAsync(city.CountryId);
                    city.Country = country;
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok(cities);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CarrierCompany comp)
        {
            if (comp == null)
            {
                return NotFound();
            }

            try
            {
                var country = await _countries.GetByIdAsync(comp.CountryId);
                comp.CountryId = country.Id;
                comp.Country = null;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            _context.Add(comp);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CarrierCompany>> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var company = await _context.CarrierCompanies
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (company == null)
            {
                return NotFound();
            }

            try
            {
                var country = await _countries.GetByIdAsync(company.CountryId);
                company.Country = country;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }


            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.CarrierCompanies
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (company == null)
            {
                return NotFound();
            }

            _context.Remove(company);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] CarrierCompany data)
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
                    var country = await _countries.GetByIdAsync(data.CountryId);
                    data.CountryId = country.Id;
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.CarrierCompanies.Any(c => c.Id == id))
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
