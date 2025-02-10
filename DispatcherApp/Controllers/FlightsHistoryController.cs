using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsHistoryController : ControllerBase
    {
        private readonly FlightsHistoryService _history;

        public FlightsHistoryController(FlightsHistoryService history)
        {
            _history = history;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetAll()
        {
            var flights = await _history.GetAllAsync();

            if (flights == null) return NotFound();

            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetById(int? id)
        {
            if (id == null) return NotFound();

            var flight = await _history.GetByIdAsync(id);

            if (flight == null) return NotFound();

            return Ok(flight);
        }

        [HttpGet("details")]
        public async Task<ActionResult<Flight>> GetByPlaneTail(string number)
        {
            if (number == null)
            {
                return NotFound();
            }

            var flight = await _history.FindLastByPlaneTailAsync(number);

            if (flight == null) return NotFound(new { message = "Самолет с таким бортовым номером не найден" });

            return Ok(flight);
        }
    }
}
