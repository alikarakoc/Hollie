using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CRoomDto
    {
        [Key]
        public bool selected { get; set; }

        public int RoomId { get; set; }

        public int ListId { get; set; }
    }
}
