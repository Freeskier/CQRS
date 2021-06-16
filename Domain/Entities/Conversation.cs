using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Conversation
    {
        [Key]
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
    }
}