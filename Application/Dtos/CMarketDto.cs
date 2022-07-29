using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CMarketDto
    {
        [Key]

        public bool selected { get; set; }

        public int MarketId { get; set; }

        public string ListId { get; set; }
    }
}
