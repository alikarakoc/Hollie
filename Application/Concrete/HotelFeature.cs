using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class HotelFeature
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string Code { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { get; set; }
        public byte BabyTop { get; set; }
        public byte ChildTop { get; set; }
        public byte TeenTop  { get; set; }
        public bool Status { get; set; }
    }
}
