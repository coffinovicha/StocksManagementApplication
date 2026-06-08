using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksManagementApplication.Core.DTOs
{
    internal class LoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
