using System;

namespace Domain.Dtos
{
    public class MessageResponse
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime DateRead { get; set; }
        public string SenderUserName { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public string ReceiverUserName { get; set; }
        public string ReceiverPhotoUrl { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
    }
}