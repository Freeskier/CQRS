using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Follow
    {
        public virtual AppUser Follower { get; set; }
        [Required]
        public Guid FollowerId { get; set; }
        public virtual AppUser Followee { get; set; }
        [Required]
        public Guid FolloweeId { get; set; }
    }
}