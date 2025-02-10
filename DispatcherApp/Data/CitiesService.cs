using DispatcherApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DispatcherApp.Data
{
    public class CitiesService
    {
        private readonly IMongoCollection<City> _cities;

        public CitiesService(IOptions<MongoSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var db = client.GetDatabase(mongoSettings.Value.Database);
            _cities = db.GetCollection<City>("Cities");
        }

        public async Task<List<City>> GetAllAsync() => await _cities.Find(city => true).ToListAsync();

        public async Task<City> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var flight = await _cities.Find(f => f.Id == id).FirstOrDefaultAsync();

            return flight;
        }

        public async Task<City> FindLastAsync()
        {
            var city = await _cities.Find(new BsonDocument())
                .Sort(Builders<City>.Sort.Descending(f => f.Id))
                .Limit(1)
                .FirstOrDefaultAsync();

            return city;
        }

        public async Task CreateAsync(City city)
        {
            await _cities.InsertOneAsync(city);
        }

        public async Task DeleteAsync(int? id)
        {
            await _cities.DeleteOneAsync(f => f.Id == id);
        }

        public async Task EditAsync(int? id, City city)
        {
            var filter = Builders<City>.Filter.Eq(f => f.Id, id);
            await _cities.ReplaceOneAsync(filter, city);
        }
    }
}
