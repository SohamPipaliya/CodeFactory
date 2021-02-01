using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFactoryAPI.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public int Tag_ID { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<UsersTags> UsersTags { get; set; }
    }
}
