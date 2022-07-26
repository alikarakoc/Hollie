﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CBoardDto
    {
        [Key]
        public int Id { get; set; }

        public int BoardId { get; set; }

        public int ListId { get; set; }
    }
}
