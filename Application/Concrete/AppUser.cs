using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
