using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BindingModel
{
    public class loginBindingModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        //public string Email { get; set; }
        public string Password { get; set; }

        //public string Token { get; set; }

    }
}
