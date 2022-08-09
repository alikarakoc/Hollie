using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int RoomTypeId { get; set; }
        public int HotelId { get; set; }
        public int Slot { get; set; }
        public string Bed { get; set; }
        public bool Status { get; set; }
    }
}
