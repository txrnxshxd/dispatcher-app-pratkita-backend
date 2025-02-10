using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherApp.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public ICollection<Airport> Airports = new List<Airport>();
        public ICollection<Pilot> Pilots = new List<Pilot>();
    }
}
