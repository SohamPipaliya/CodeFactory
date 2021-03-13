using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeFactoryAPI.Models
{
    [Keyless]
    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(User user)
        {
            User_ID = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            RegistrationDate = user.RegistrationDate;
            Image = user.Image;
        }

        public string? User_ID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string? Image { get; set; }
    }
}
