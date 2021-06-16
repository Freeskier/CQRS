using System;

namespace Domain.Dtos
{
    public class MessageRequest
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
    }
}