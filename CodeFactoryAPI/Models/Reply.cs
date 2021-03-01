using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFactoryAPI.Models
{
    [Table("Replies")]
    public class Reply
    {
        [Key]
        public Guid Reply_ID { get; set; }

        [Required]
        public string Message { get; set; }

        public string? Code { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image1 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image2 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image3 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image4 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image5 { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RepliedDate { get; set; }

        [Required]
        public Guid? User_ID { get; set; }

        [ForeignKey("User_ID")]
        public User? User { get; set; }

        [Required]
        public Guid? Question_ID { get; set; }

        [ForeignKey("Question_ID")]
        public Question? Question { get; set; }
    }
}
