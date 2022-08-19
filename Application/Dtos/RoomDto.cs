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
        public short HotelId { get; set; }
        public short RoomTypeId { get; set; }
        public bool Clean { get; set; }
        public bool Reservation { get; set; }
        public bool Status { get; set; }
    }
}
