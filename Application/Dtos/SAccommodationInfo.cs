using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class SAccommodationInfo
    {
        public float TotalPrice { get; set; }
        public List<ContractDto> ContractList { get; set; }
    }
}
