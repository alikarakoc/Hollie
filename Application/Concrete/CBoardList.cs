using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class CBoardList
    {
        [Key]
        public int Id { get; set; }

        public int BoardId { get; set; }

        public int ListId { get; set; }
    }
}
