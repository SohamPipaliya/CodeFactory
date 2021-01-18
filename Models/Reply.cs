using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Model
{
    [Table("Reply")]
    public class Reply
    {
        [Key]
        public Guid Reply_ID { get; set; }

        [Required]
        public string Message { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image2 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image3 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image4 { get; set; }

        [DataType(DataType.ImageUrl)]
        public string? Image5 { get; set; }

        [Required]
        public Guid User_ID { get; set; }

        [ForeignKey("User_ID")]
        public User user { get; set; }
    }
}
