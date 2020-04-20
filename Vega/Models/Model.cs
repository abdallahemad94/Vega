﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
