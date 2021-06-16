using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repos
{
    public interface IFollowRepo : IGenericRepo<Follow>
    {
        Task<IEnumerable<Follow>> GetFolloweesForUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Follow>> GetFollowersForUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Follow> GetSingleFollowAsync(Guid followerId, Guid followeeId);
    }
}