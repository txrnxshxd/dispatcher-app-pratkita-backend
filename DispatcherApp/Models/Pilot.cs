using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherApp.Models
{
    public class Pilot
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string? MiddleName { get; set; }

        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }


        public ICollection<Flight> Flights = new List<Flight>();
    }
}
