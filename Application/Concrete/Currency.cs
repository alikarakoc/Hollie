using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Code { get; set; }
        [StringLength(100)]
        [Column(TypeName = "nvarchar")]
        public string? Name { get; set; }

        [Column(TypeName = "float")]
        public float? Unit { get; set; }
        [Column(TypeName = "float")]
        public float? Value { get; set; }
        [Column(TypeName = "date")]
        public DateTime CurrencyDate { get; set; }


    }
}
