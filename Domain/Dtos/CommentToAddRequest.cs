using System;

namespace Domain.Dtos
{
    public class CommentToAddRequest
    {
        public Guid PhotoId { get; set; }
        public string Content { get; set; }
    }
}