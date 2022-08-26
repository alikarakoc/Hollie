using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class Role : IdentityRole<int>
    {
        public DateTime DateTime { get; set; }
    }
}
