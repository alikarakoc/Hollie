using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ContractDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int HotelId { get; set; }
        public int MarketId { get; set; }
        public int AgencyId { get; set; }
        public int BoardId { get; set; }
        public int RoomTypeId { get; set; }

        public DateTime EnteredDate { get; set; }
        public DateTime ExitDate { get; set; }
        public int price { get; set; }
        public int currency { get; set; }
    }
}
