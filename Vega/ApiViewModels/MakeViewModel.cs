using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.ApiViewModels
{
    public class MakeViewModel
    {
        public MakeViewModel()
        {
            Models = new List<ModelViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ModelViewModel> Models { get; set; }
    }
}
