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

        [Required]
        public Guid Tag1_ID { get; set; }

        [ForeignKey("Tag1_ID")]
        public Tag? Tag1 { get; set; }

        [Required]
        public Guid Tag2_ID { get; set; }

        [ForeignKey("Tag2_ID")]
        public Tag? Tag2 { get; set; }

        public Guid? Tag3_ID { get; set; }

        [ForeignKey("Tag3_ID")]
        public Tag? Tag3 { get; set; }

        public Guid? Tag4_ID { get; set; }

        [ForeignKey("Tag4_ID")]
        public Tag? Tag4 { get; set; }

        public Guid? Tag5_ID { get; set; }

        [ForeignKey("Tag5_ID")]
        public Tag? Tag5 { get; set; }

        public IEnumerable<Reply>? Replies { get; set; }

        public IEnumerable<Message>? Messages { get; set; }
    }
}
