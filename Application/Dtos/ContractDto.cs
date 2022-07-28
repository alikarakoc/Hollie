using Application.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ContractDto
    {
        public ContractDto()
        {
            AgencyList = new List<CAgencyList>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int HotelId { get; set; }
        //public int MarketId { get; set; }
        //public int BoardId { get; set; }
        public int RoomTypeId { get; set; }
        public int CurrencyId { get; set; }

        public DateTime EnteredDate { get; set; }
        public DateTime ExitDate { get; set; }
        public float Price { get; set; }
        //public int Currency { get; set; }
        public List<CAgencyList> AgencyList { get; set; }
        public List<CMarketList> MarketList { get; set; }
        public List<CBoardList> BoardList { get; set; }
        
    }
}
