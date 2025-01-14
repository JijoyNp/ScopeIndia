using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ScopeIndia.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage ="fill the field")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "fill the field")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [DataType(DataType.Text)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "fill the field")]
        [DataType(DataType.Text)]
        public string Message { get; set; }




    }
}
