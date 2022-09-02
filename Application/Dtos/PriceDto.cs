using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PriceDto
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public float totalPrice { get; set; }
        public int HotelId { get; set; }
    }
}
