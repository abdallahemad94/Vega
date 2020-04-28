using System;
using System.Collections.Generic;

namespace Vega.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        public int ModelId { get; set; }

        public Model Model { get; set; }

        public bool IsRegistered { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string ContactEmail { get; set; }

        public DateTime LastUpdated { get; set; }

        public IList<VehicleFeature> Features { get; set; } = new List<VehicleFeature>();

    }
}
