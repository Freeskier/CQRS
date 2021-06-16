using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PhotoLike
    {
        [Required]
        public Guid LikerId { get; set; }
        public virtual AppUser Liker { get; set; }
        [Required]
        public Guid PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
        public DateTime DateLiked { get; set; }

        public PhotoLike()
        {   
            DateLiked = DateTime.UtcNow;
        }
    }
}