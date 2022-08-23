using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Username { get; set; }

    }
}
