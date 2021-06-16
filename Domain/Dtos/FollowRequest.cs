using System;

namespace Domain.Dtos
{
    public class FollowRequest
    {
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }
    }
}