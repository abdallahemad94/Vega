using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Models
{
    public class Make
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Model> Models { get; set; } = new List<Model>();
    }
}
