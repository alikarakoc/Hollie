using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class SearchAccommodationDate
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte Adult { get; set; }
        public byte NumberOfChild { get; set; }
        public byte Child1Age { get; set; }
        public byte Child2Age { get; set; }
        public byte Child3Age { get; set; }
        public List<int> Hotels { get; set; }
    }
}
