using System;

namespace Domain.Dtos
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserMainPhotoUrl { get; set; }
        public string UserUserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}