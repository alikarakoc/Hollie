using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class Currency
    {
        [Key]
        public int id { get; set; }
        [StringLength(20)]
        public string Code { get; set; }
        [StringLength(20)]
        public string name { get; set; }

    }
}
