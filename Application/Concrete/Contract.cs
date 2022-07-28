using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class Contract
    {
        public Contract()
        {
            AgencyList = new List<CAgencyList>();
        }
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(20)]
        public string Code { get; set; }
        public int HotelId { get; set; }
        public int MarketId { get; set; }
        public int AgencyId { get; set; }
        public int BoardId { get; set; }
        public int RoomTypeId { get; set; }
        public float Price { get; set; }
        public int CurrencyId { get; set; }
        public DateTime EnteredDate { get; set; }
        public DateTime ExitDate { get; set; }
        [NotMapped]
        public List<CAgencyList> AgencyList { get; set; }

        [NotMapped]
        public List<CBoardList> BoardList { get; set; }



    }
}
