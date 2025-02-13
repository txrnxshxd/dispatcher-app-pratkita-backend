using DispatcherApp.Data;
using DispatcherApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace DispatcherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly RadarService _radar;
        private readonly CitiesService _cities;
        private readonly ApplicationDbContext _context;
        private readonly FlightsHistoryService _history;

        public FlightsController(RadarService radar, CitiesService cities, ApplicationDbContext context, FlightsHistoryService history)
        {
            _radar = radar;
            _cities = cities;
            _context = context;
            _history = history;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetAll()
        {
            var flights = await _radar.GetAllAsync();

            return Ok(flights);
        }

        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<Flight>>> GetByPlaneTail(int? id, string number)
        {
            if (number == null)
            {
                return NotFound();
            }

            var flights = await _radar.GetByPlaneTailAsync(number);

            var existingFlight = flights.Where(f => f.Speed > 0 && f.Speed != null).FirstOrDefault();

            if (existingFlight != null)
            {
                return Ok(existingFlight);
            }

            if (id.HasValue)
            {
                var flightById = await _radar.GetByIdAsync(id.Value);
                if (flightById != null)
                {
                    return Ok(flightById);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Flight flight)
        {

            var lastFlight = await _radar.GetLastAsync();

            if (lastFlight == null)
            {
                flight.Id = 1;
            }
            else
            {
                flight.Id = lastFlight.Id + 1;
            }

            var existingFlight = await _radar.GetLastByPlaneTailAsync(flight.Plane.TailNumber);
            var lastHistoryFlight = await _history.GetLastByPlaneTailAsync(flight.Plane.TailNumber);

            if (existingFlight != null)
            {
                flight.Latitude = existingFlight.AirportTo.Latitude;
                flight.Longitude = existingFlight.AirportTo.Longitude;
            }
            else
            {
                if (lastHistoryFlight != null)
                {
                    flight.Latitude = lastHistoryFlight.AirportTo.Latitude;
                    flight.Longitude = lastHistoryFlight.AirportTo.Longitude;
                }
                else
                {
                    flight.Latitude = flight.AirportFrom.Latitude;
                    flight.Longitude = flight.AirportFrom.Longitude;
                }
            }

            await _radar.CreateFlightAsync(flight);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                await _radar.DeleteFlightAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound();
            }


            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Flight data)
        {
            if (id != data.Id)
            {
                return NotFound("Не найдено!");
            }

            if (data.Fullness > data.Plane.PassengerCapacity)
            {
                return NotFound(new { message = $"Максимальное количество пассажиров: {data.Plane.PassengerCapacity}, Вы указали: {data.Fullness}" });
            }

            try
            {
                await _radar.EditFlightAsync(id, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Ok();
        }
    }
}
