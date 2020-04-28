using System.Collections.Generic;

namespace Vega.Models
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MakeId { get; set; }
        public Make Make { get; set; }

        public IList<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
