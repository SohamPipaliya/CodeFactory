using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFactoryAPI.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public Guid Message_ID { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        [StringLength(maximumLength: Int32.MaxValue, MinimumLength = 25, ErrorMessage = "Add some more text")]
        public string Messages { get; set; }

        public DateTime MessageDate { get; set; }

        [Required]
        public Guid? Messeger_ID { get; set; }

        [ForeignKey("Messeger_ID")]
        public User? Messeger { get; set; }

        [Required]
        public Guid? Receiver_ID { get; set; }

        [ForeignKey("Receiver_ID")]
        public User? Receiver { get; set; }

        [Required]
        public Guid Question_ID { get; set; }

        [ForeignKey("Question_ID")]
        public Question? Question { get; set; }
    }
}
