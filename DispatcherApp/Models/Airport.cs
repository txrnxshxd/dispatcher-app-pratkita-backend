using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DispatcherApp.Models
{
    public class Airport
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(4)]
        public string ICAO { get; set; } = string.Empty;
        [MaxLength(3)]
        public string IATA { get; set; } = string.Empty;
        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
