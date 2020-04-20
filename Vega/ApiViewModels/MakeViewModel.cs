using System.Collections.Generic;

namespace Vega.ApiViewModels
{
    public class MakeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ModelViewModel> Models { get; set; } = new List<ModelViewModel>();
    }
}
