using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class Room
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string Code { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Name { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public int slot { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(7)]
        public string bed { get; set; }
        public bool status { get; set; }

    }
}
