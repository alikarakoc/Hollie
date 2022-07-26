using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CAgencyDto
    {
        [Key]
        public int Id { get; set; }

        public int AgencyId { get; set; }

        public string ListId { get; set; }
    }
}
