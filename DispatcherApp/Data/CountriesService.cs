using DispatcherApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DispatcherApp.Data
{
    public class CountriesService
    {
        private readonly IMongoCollection<Country> _countries;

        public CountriesService(IOptions<MongoSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            var db = client.GetDatabase(mongoSettings.Value.Database);
            _countries = db.GetCollection<Country>("Countries");
        }

        public async Task<List<Country>> GetAllAsync() => await _countries.Find(country => true).ToListAsync();

        public async Task<Country> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var country = await _countries.Find(f => f.Id == id).FirstOrDefaultAsync();

            return country;
        }

        public async Task<Country> FindLastAsync()
        {
            var country = await _countries.Find(new BsonDocument())
                .Sort(Builders<Country>.Sort.Descending(f => f.Id))
                .Limit(1)
                .FirstOrDefaultAsync();

            return country;
        }

        public async Task CreateAsync(Country country)
        {
            await _countries.InsertOneAsync(country);
        }

        public async Task DeleteAsync(int? id)
        {
            await _countries.DeleteOneAsync(f => f.Id == id);
        }

        public async Task EditAsync(int? id, Country country)
        {
            var filter = Builders<Country>.Filter.Eq(f => f.Id, id);
            await _countries.ReplaceOneAsync(filter, country);
        }
    }
}
