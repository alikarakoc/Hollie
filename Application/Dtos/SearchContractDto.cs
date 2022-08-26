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
    }
}
