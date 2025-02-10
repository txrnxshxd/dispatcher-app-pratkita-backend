using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherApp.Models
{
    public class CarrierCompany
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }

        public ICollection<Flight> Flights = new List<Flight>();
    }
}
