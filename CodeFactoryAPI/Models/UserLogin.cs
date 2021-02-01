using System.ComponentModel.DataAnnotations;

namespace CodeFactoryAPI.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "UserName is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Length between 5 to 25 is valid")]
        [RegularExpression(pattern: "^[A-Za-z0-9_@]+$", ErrorMessage = "Consider the Format")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Length between 5 to 25 is valid")]
        [RegularExpression(pattern: "^[A-Za-z0-9_@*]+$", ErrorMessage = "Consider the Format")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
