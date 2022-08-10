using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Concrete
{
    public class Authentication 
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string ConfirmPassword { get; set; }
    }
}
