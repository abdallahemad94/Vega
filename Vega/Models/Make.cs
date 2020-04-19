using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vega.Models
{
    [Table("Makes")]
    public class Make
    {
        public Make()
        {
            Models = new List<Model>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public IList<Model> Models { get; set; }
    }
}
