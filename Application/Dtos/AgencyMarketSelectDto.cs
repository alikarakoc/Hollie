using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class AgencyMarketSelectDto
    {
        public int AgencyId { get; set; }
        public string AgencyCode { get; set; }
        public List<int> Markets { get; set; }
    }
}
