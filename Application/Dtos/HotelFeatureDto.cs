using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class HotelFeatureDto
    {
       
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public byte BabyTop { get; set; }
        public byte ChildTop { get; set; }
        public byte TeenTop { get; set; }
        public bool Status { get; set; }
    }
}
