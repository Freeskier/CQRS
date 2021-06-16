using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Photo
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Url { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<PhotoLike> LikesReceived { get; set; }
        public virtual ICollection<Comment> CommentsReceived { get; set; }

        public Photo()
        {
            DateAdded = DateTime.UtcNow;
        }
    }
}