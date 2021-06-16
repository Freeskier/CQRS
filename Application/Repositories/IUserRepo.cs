using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repos
{
    public interface IUserRepo : IGenericRepo<AppUser>
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<AppUser> GetSingleUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<AppUser> GetSingleUserByUsernameAsync(string userName, CancellationToken cancellationToken = default);
        Task<AppUser> GetSingleUserByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<IEnumerable<AppUser>> GetUsersWithKeyWord(string keyWord, CancellationToken cancellationToken = default);
    }
}