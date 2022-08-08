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
        public int slot { get; set; }
        public string bed { get; set; }
        public bool status { get; set; }
    }
}
