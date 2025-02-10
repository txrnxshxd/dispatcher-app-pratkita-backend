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

            var lastFlight = await _radar.FindLastAsync();

            if (lastFlight == null)
            {
                flight.Id = 1;
            }
            else
            {
                flight.Id = lastFlight.Id + 1;
            }

            var existingFlight = await _radar.GetLastByPlaneTailAsync(flight.Plane.TailNumber);
            var lastHistoryFlight = await _history.FindLastByPlaneTailAsync(flight.Plane.TailNumber);

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


            //flight.AirportFrom = await _context.Airports.Where(a => a.Id == flight.From).FirstOrDefaultAsync();

            //if (flight.AirportFrom == null) return NotFound();

            //flight.AirportTo = await _context.Airports.Where(a => a.Id == flight.To).FirstOrDefaultAsync();

            //if (flight.AirportTo == null) return NotFound();

            //flight.AirportFrom.City = city1;
            //flight.AirportTo.City = city2;

            //flight.Plane = await _context.Planes.Where(p => p.Id == flight.PlaneId).FirstOrDefaultAsync();

            //if (flight.Plane == null) return NotFound();

            //flight.Captain = await _context.Pilots.Where(p => p.Id == flight.CaptainId).FirstOrDefaultAsync();

            //if (flight.Captain == null) return NotFound();

            //flight.Pilot = await _context.Pilots.Where(p => p.Id == flight.PilotId).FirstOrDefaultAsync();

            //if (flight.Pilot == null) return NotFound();

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
