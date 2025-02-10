namespace DispatcherApp.Models
{
    public class PlaneType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Plane> Planes = new List<Plane>();
    }
}
