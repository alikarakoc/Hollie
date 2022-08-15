using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [StringLength(100)]
        [Column(TypeName = "nvarchar")]
        public string? Name { get; set; }
        [Column(TypeName = "float")]
        public float? Unit { get; set; }
        [Column(TypeName = "float")]
        public float? Value { get; set; }

        public DateTime CurrencyDate { get; set; }
    }
}
