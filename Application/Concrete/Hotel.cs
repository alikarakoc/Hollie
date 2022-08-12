using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class Hotel : BaseEntity
    {
      
        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string Code { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(200)]
        public string Address { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string Phone { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Email { get; set; }
        public int HotelCategoryId { get; set; }
        public int HotelFeatureId { get; set; }

        public bool Status { get; set; }

    }
}

