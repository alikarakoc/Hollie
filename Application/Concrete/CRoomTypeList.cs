using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class CRoomTypeList
    {
        [Key]
        public int Id { get; set; }

        public int RoomTypeId { get; set; }

        public int ListId { get; set; }
    }
}
