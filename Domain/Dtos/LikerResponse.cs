using System;

namespace Domain.Dtos
{
    public class LikerResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MainPhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}