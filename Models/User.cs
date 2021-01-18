using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid User_ID { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Length between 5 to 25 is valid")]
        [RegularExpression(pattern: "^[A-Za-z0-9_@]+$", ErrorMessage = "Consider the Format")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Length between 5 to 25 is valid")]
        [RegularExpression(pattern: "^[A-Za-z0-9_@*]+$", ErrorMessage = "Consider the Format")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image { get; set; }
    }
}
