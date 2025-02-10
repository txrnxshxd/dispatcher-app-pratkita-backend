using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherApp.Models
{
    public class Plane
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(6)]
        public string TailNumber { get; set; } = string.Empty;
        public int PlaneTypeId { get; set; }
        public PlaneType PlaneType { get; set; }
        public int TakeoffSpeed { get; set; }
        public int CruisingSpeed { get; set; }
        public int LandingSpeed { get; set; }
        public int MaxAltitude { get; set; }
        public int PassengerCapacity { get; set; }
        public int MaxSpeed { get; set; }
        public int ManufactureYear { get; set; }
        public DateOnly LastCheckDate { get; set; }

        public ICollection<Flight> Flights = new List<Flight>();
    }
}
