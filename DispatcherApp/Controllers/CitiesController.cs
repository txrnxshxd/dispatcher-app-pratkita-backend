using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly CitiesService _cities;

        public CitiesController(CitiesService cities)
        {
            //_context = context;
            _cities = cities;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAll()
        {
            var cities = await _cities.GetAllAsync();

            if (cities == null)
            {
                return NotFound();
            }

            return Ok(cities);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] City city)
        {
            if (city == null) 
            {
                return NotFound();
            }

            var lastElement = await _cities.FindLastAsync();

            if (lastElement == null)
            {
                city.Id = 1;
            }
            else
            {
                city.Id = lastElement.Id + 1;
            }

            await _cities.CreateAsync(city);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _cities.GetByIdAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            };

            await _cities.DeleteAsync(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] City data)
        {
            if (id != data.Id)
            {
                return NotFound();
            }

            string oldName = data.Name;

            try
            {
                await _cities.EditAsync(id, data);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
