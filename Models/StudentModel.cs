using System.ComponentModel.DataAnnotations;

namespace ScopeIndia.Models
{
    public class StudentModel
    {
        [Required(ErrorMessage="Fill the field")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Fill the field")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Fill the field")]
        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Fill the field")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Fill the field")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fill the field")]
        [DataType(DataType.PhoneNumber)]
        public string PhNo { get; set; }

        
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Fill the field")]
        [DataType(DataType.Text)]
        public string State { get; set; }

        
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "Select atleast one!")]
        [DataType(DataType.Text)]
        public string[]? Hobbies { get; set; }

        public string? AllHobbies { get; set; }

        public IFormFile Avatar { get; set; }

        public string? Avatarpath {  get; set; }

    }
}
