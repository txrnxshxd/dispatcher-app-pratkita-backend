using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using MongoDB.Driver.Linq;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly CitiesService _cities;
        private readonly CountriesService _countries;

        public AirportsController(ApplicationDbContext context, CitiesService cities, CountriesService countries)
        {
            _context = context;
            _cities = cities;
            _countries = countries;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airport>>> GetAll()
        {
            var airports = await _context.Airports
                .ToListAsync();

            if (airports == null)
            {
                return NotFound();
            }

            foreach (var airport in airports)
            {
                try
                {
                    var city = await _cities.GetByIdAsync(airport.CityId);
                    airport.City = city;

                    var country = await _countries.GetByIdAsync(airport.City.CountryId);
                    airport.City.Country = country;

                }
                catch (Exception ex)
                {
                    return NotFound();
                }
            }


            return Ok(airports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Airport>> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (airport == null) return NotFound();

            var city = await _cities.GetByIdAsync(airport.CityId);

            if (city == null) return NotFound();

            airport.City = city;

            return Ok(airport);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Airport airport)
        {
            if (airport == null)
            {
                return NotFound();
            }

            airport.City = null;

            _context.Add(airport);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (airport == null)
            {
                return NotFound();
            }

            _context.Remove(airport);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Airport data)
        {
            if (id != data.Id)
            {
                return NotFound();
            }

            _context.Entry(data).State = EntityState.Modified;

            var city = await _cities.GetByIdAsync(data.CityId);

            if (city == null)
            {
                return NotFound();
            }

            var country = city.Country;

            if (country == null)
            {
                return NotFound();
            }

            data.CityId = city.Id;
            data.City.CountryId = country.Id;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Airports.Any(c => c.Id == id))
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
