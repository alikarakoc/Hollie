using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class RoomType : BaseEntity
    {

        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string Code { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "float")]
        public short HotelId { get; set; }
        [Column(TypeName = "smallint")]
        public short MaxAD { get; set; }
        [Column(TypeName = "smallint")]
        public short MaxCH { get; set; }

        [Column(TypeName = "smallint")]
        public short Pax { get; set; }
        public bool Status { get; set; }


    }
}
