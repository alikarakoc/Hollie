using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class MarketAgencySelectorDto
    {
        public int MarketId { get; set; }
        public List<int> Agencies { get; set; }
    }
}
