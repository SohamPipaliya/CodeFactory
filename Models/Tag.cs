using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Model
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public int Tag_ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
