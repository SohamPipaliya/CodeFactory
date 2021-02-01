using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFactoryAPI.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        public Guid Question_ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Code { get; set; }

        public string? Image1 { get; set; }

        public string? Image2 { get; set; }

        public string? Image3 { get; set; }

        public string? Image4 { get; set; }

        public string? Image5 { get; set; }

        public DateTime AskedDate { get; set; }

        public Guid User_ID { get; set; }

        [ForeignKey("User_ID")]
        public User? User { get; set; }

        public IList<UsersTags> UsersTags { get; set; }
    }
}
