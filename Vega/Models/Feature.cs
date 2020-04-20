using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Models
{
    public class Feature
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<VehicleFeature> Vehicles { get; set; } = new List<VehicleFeature>();
    }
}
