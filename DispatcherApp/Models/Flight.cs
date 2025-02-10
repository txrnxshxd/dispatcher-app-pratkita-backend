using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherApp.Models
{
    public class Flight
    {
        [BsonId]
        public int Id { get; set; }

        public int CarrierCompanyId { get; set; }
        [ForeignKey(nameof(CarrierCompanyId))]
        public CarrierCompany CarrierCompany { get; set; }

        public int PlaneId { get; set; }
        [ForeignKey(nameof(PlaneId))]
        public Plane Plane { get; set; }

        public int From { get; set; }
        [ForeignKey(nameof(From))]
        public Airport AirportFrom { get; set; }

        public int To { get; set; }
        [ForeignKey(nameof(To))]
        public Airport AirportTo { get; set; }

        public int CaptainId { get; set; }
        [ForeignKey(nameof(CaptainId))]
        public Pilot Captain { get; set; }

        public int PilotId { get; set; }
        [ForeignKey(nameof(PilotId))]
        public Pilot Pilot { get; set; }

        public int Fullness { get; set; }

        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int? Altitude { get; set; }

        [Range(0, 500, ErrorMessage = "Скорость не может быть выше 500")]
        public int? Speed { get; set; }
        public int? VerticalSpeed { get; set; }

        [Range(0, 360, ErrorMessage = "Курс не может быть выше 360 градусов")]
        public int? Course { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? DistanceBetweenAirportsKm { get; set; }
        public double? DistanceBetweenAirportsM { get; set; }
    }
}
