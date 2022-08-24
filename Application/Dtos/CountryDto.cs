﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UpdateUser { get; set; }
        public bool Status { get; set; }

    }
}
