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
        public int CurrencyId { get; set; }

        public DateTime EnteredDate { get; set; }
        public DateTime ExitDate { get; set; }
        public bool Status { get; set; }

        public float ADP { get; set; }
        public float CH1 { get; set; }
        public float CH2 { get; set; }
        public float CH3 { get; set; }

        public List<CAgencyList> AgencyList { get; set; }
        public List<CMarketList> MarketList { get; set; }
        public List<CBoardList> BoardList { get; set; }
        public List<CRoomTypeList> RoomTypeList { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedUser { get; set; }


    }
}
