using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vega.ApiViewModels
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ModelId { get; set; }

        public bool IsRegestired { get; set; } = false;

        [Required]
        public ContactViewodel ContactInfo { get; set; }

        public IList<int> Features { get; set; } = new List<int>();
    }
}
