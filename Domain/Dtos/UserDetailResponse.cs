using System;

namespace Domain.Dtos
{
    public class UserDetailResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string MainPhotoUrl { get; set; }
        public string City { get; set; }     
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JoinDate { get; set; }
        public string Token { get; set; }
    }
}