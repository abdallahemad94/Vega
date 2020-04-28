using System;
using System.Collections.Generic;

namespace Vega.ApiViewModels
{
    public class VehicleResource
    {
        public int Id { get; set; }

        public KeyValuePairResource Model { get; set; }

        public MakeResource Make { get; set; }

        public bool IsRegistered { get; set; }

        public ContactResource ContactInfo { get; set; }

        public DateTime LastUpdated { get; set; }

        public IList<KeyValuePairResource> Features { get; set; } = new List<KeyValuePairResource>();
    }
}
