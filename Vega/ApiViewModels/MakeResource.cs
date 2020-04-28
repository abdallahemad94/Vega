using System.Collections.Generic;

namespace Vega.ApiViewModels
{
    public class MakeResource : KeyValuePairResource
    {
        public IList<KeyValuePairResource> Models { get; set; } = new List<KeyValuePairResource>();
    }
}
