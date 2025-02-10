using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly CountriesService _countries;

        public CountriesController(CountriesService countries)
        {
            //_context = context;
            _countries = countries;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get()
        {
            var result = await _countries.GetAllAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Country country)
        {
            var lastElement = await _countries.FindLastAsync();

            if (lastElement == null)
            {
                country.Id = 1;
            }
            else
            {
                country.Id = lastElement.Id + 1;
            }

            await _countries.CreateAsync(country);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound("Неверный Id");
            }

            var country = await _countries.GetByIdAsync(id);

            if (country == null)
            {
                return NotFound(new { message = "Страна не найдена"});
            }

            return Ok(country);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _countries.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Country data)
        {
            if (id != data.Id)
            {
                return NotFound();
            }

            try
            {
                await _countries.EditAsync(id, data);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
