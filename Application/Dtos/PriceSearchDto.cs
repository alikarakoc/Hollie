using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PriceSearchDto
    {
        public PriceSearchDto()
        {
            PriceDetails = new List<PriceSearchDetail>();
        }
        public int AgencyId { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int Adult { get; set; }
        public int NumberOfChild { get; set; }
        //public int Baby { get; set; }
        //public int Child { get; set; }
        //public int Teen { get; set; }
        public int[] ChildAges { get; set; }
        public string AgencyName { get; set; }
        public string HotelName { get; set; }
        public string RoomName { get; set; }
        public double TotalPrice { get; set; }
        public List<PriceSearchDetail> PriceDetails { get; set; }


    }
    public class PriceSearchDetail
    {
        public int StayDay { get; set; }
        public double BasePrice { get; set; }
        //public double Discounts { get; set; }
        public double  NetPrice { get; set; }
        public int ContractId { get; set; }
        public string ContractCode { get; set; }
    }
}
