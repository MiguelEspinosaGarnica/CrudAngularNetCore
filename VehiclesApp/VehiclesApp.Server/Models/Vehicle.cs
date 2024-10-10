namespace VehiclesApp.Server.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int SeatsNumber { get; set; }
        public string? Price { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public int CylinderCapacity { get; set; }
    }
}
