using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }
        [Required]
        public Guid PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
        [Required, MaxLength(400)]
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }

        public Comment()
        {
            DateAdded = DateTime.UtcNow;
        }
    }
}