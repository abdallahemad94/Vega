using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vega.ApiViewModels
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }

        [Required]
        public int ModelId { get; set; }

        public bool IsRegistered { get; set; }

        [Required]
        public ContactResource ContactInfo { get; set; }

        public IList<int> Features { get; set; } = new List<int>();
    }
}
