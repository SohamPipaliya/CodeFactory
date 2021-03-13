using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFactoryAPI.Models
{
    [Table("Users")]
    public class User : IdentityUser
    {
        public User() { }

        public User(UserViewModel userView)
        {
            Id = userView.User_ID;
            UserName = userView.UserName;
            Email = userView.Email;
            Image = userView.Image;
            RegistrationDate = userView.RegistrationDate;
        }

        [DataType(DataType.DateTime)]
        public DateTime? RegistrationDate { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image { get; set; }
    }
}
