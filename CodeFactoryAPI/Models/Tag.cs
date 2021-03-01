using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFactoryAPI.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public Guid? Tag_ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
