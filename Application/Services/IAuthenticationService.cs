using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthenticationService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        Task<string> CreateToken(Guid id, string userName, string email);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}