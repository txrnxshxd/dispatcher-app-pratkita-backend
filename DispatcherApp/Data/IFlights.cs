using DispatcherApp.Models;

namespace DispatcherApp.Data
{
    public interface IFlights
    {
        public Task<List<Flight>> GetAllAsync();
        public Task<List<Flight>> GetByPlaneTailAsync(string? number);
        public Task<Flight> GetByIdAsync(int? id);
        public Task<Flight> GetLastByPlaneTailAsync(string? tailnum);
        public Task<Flight> GetLastAsync();
        public Task CreateFlightAsync(Flight flight);
        public Task DeleteFlightAsync(int? id);
    }

    public interface IFlightsActive : IFlights
    {
        public Task EditFlightAsync(int? id, Flight flight);
    }
}
