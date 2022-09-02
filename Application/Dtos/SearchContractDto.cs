using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class SearchContractDto
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> Hotels { get; set; }
        public int HotelId { get; set; }
        public int adult { get; set; }
        public int child1Age { get; set; }
        public int child2Age { get; set; }
        public int child3Age { get; set; }
    }
}
