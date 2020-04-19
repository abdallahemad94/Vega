﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.ApiViewModels
{
    [Table("Models")]
    public class ModelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
