using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RoomTypeDto

    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        public short HotelId { get; set; }
        public short MaxAD { get; set; }
        public short MaxCH { get; set; }
        public short Pax { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public bool Status { get; set; }


    }
}
