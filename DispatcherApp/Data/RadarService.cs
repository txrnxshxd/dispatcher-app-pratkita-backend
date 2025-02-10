using DispatcherApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DispatcherApp.Data
{
    public class RadarService
    {
        private readonly IMongoCollection<Flight> _flights;

        public RadarService(IOptions<MongoSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var db = client.GetDatabase(mongoSettings.Value.Database);
            _flights = db.GetCollection<Flight>("Flights");
        }

        public async Task<List<Flight>> GetAllAsync() => await _flights.Find(flight => true).ToListAsync();

        public async Task<List<Flight>> GetByPlaneTailAsync(string? number)
        {
            if (number == null)
            {
                return null;
            }

            var flight = await _flights.Find(f => f.Plane.TailNumber == number).ToListAsync();

            return flight;
        }

        public async Task<bool> IsInAir(string? number)
        {
            if (number == null)
            {
                return false;
            }

            var flight = await _flights.Find(new BsonDocument())
                .Sort(Builders<Flight>.Sort.Descending(f => f.Speed != null || f.Speed > 0))
                .Limit(1)
                .FirstOrDefaultAsync();

            if (flight == null)
            {
                return false;
            }

            return true;
        } 

        public async Task<Flight> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var flight = await _flights.Find(f => f.Id == id).FirstOrDefaultAsync();

            return flight;
        }

        public async Task<Flight> GetLastByPlaneTailAsync(string tailnum)
        {
            var filter = Builders<Flight>.Filter.Eq(f => f.Plane.TailNumber, tailnum);

            var flight = await _flights.Find(filter)
                .Sort(Builders<Flight>.Sort.Descending(f => f.Id))
                .Limit(1)
                .FirstOrDefaultAsync();

            return flight;
        }

        public async Task<Flight> FindLastAsync()
        {
            var flight = await _flights.Find(new BsonDocument())
                .Sort(Builders<Flight>.Sort.Descending(f => f.Id))
                .Limit(1)
                .FirstOrDefaultAsync();

            return flight;
        }

        public async Task CreateFlightAsync(Flight flight)
        {
            await _flights.InsertOneAsync(flight);
        }

        public async Task DeleteFlightAsync(int? id)
        {
            await _flights.DeleteOneAsync(f => f.Id == id);
        }

        public async Task EditFlightAsync(int? id, Flight flight)
        {
            var filter = Builders<Flight>.Filter.Eq(f => f.Id, id);
            await _flights.ReplaceOneAsync(filter, flight);
        }
    }
}
