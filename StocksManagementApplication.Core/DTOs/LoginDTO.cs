using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksManagementApplication.Core.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Email can't be empty")]
        [EmailAddress(ErrorMessage ="Email should be in a proper format")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Password can't be empty")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
