using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ScopeIndia.Models
{
    public class NewUserModel
    {

        public string Email { get; set; }

        [Required(ErrorMessage = "fill the field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "fill the field")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
