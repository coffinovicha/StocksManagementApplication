using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksManagementApplication.Core.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Name can't be blank")]
        public required string Name { get; set; }
        [EmailAddress(ErrorMessage = "Enter an Email in a valid format")]
        [Required(ErrorMessage = "Email Can't be blank")]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "IsMailExistent", controller: "Account", ErrorMessage ="Mail is already in use")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Phone number can't be blank")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number should only contain digits")]
        [DataType(DataType.PhoneNumber)]
        public required string Phone { get; set; }
        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Required(ErrorMessage = "Password confirmation can't be blank")]
        [Compare("Password", ErrorMessage = "Paswwords don't match")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

    }
}
