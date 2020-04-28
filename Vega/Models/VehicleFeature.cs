using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Models
{
    [Table("VehiclesFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int FeatureId { get; set; }
        public FeatureModel Feature { get; set; }
    }
}
