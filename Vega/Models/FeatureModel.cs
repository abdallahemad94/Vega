using System.Collections.Generic;

namespace Vega.Models
{
    public class FeatureModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<VehicleFeature> Vehicles { get; set; } = new List<VehicleFeature>();
    }
}
