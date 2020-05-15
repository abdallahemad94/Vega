namespace Vega.Models
{
    public class VehiclePhoto
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        public int  VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}