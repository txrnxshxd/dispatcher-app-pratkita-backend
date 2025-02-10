namespace DispatcherApp.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<CarrierCompany> CarrierCompanies = new List<CarrierCompany>();

        public ICollection<City> Cities = new List<City>();
    }
}
