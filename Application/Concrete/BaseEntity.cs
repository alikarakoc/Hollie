using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
       
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string CreatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
       
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string UpdatedUser { get; set; }
    }
}
