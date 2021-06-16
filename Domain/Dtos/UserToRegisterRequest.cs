using System;

namespace Domain.Dtos
{
    public class UserToRegisterRequest
    {
        public string Email { get; set; }
        public string ConfirmEmail { get; set; }
        public string UserName { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime BirthDate { get; set; }
    }
}