using System;

namespace Domain.Dtos
{
    public class PhotoLikeRequest
    {
        public Guid PhotoId { get; set; }
        public Guid UserId { get; set; }
    }
}