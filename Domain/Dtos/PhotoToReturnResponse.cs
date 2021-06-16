using System;

namespace Domain.Dtos
{
    public class PhotoToReturnResponse
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid UserId { get; set; }
        public string UserUserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }    
        public string UserPhotoUrl { get; set; }
    }
}