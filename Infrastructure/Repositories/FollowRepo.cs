using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repos
{
    public class FollowRepo : GenericRepo<Follow>, IFollowRepo
    {
        public FollowRepo(ApplicationDataContext context) : base(context) { }

        public async Task<Follow> GetSingleFollowAsync(Guid followerId, Guid followeeId)
        {
            var follow = await _dataContext.Follows.FindAsync(followerId, followeeId);

            return follow;
        }

        public async Task<IEnumerable<Follow>> GetFollowersForUserAsync(Guid userId, 
            CancellationToken cancellationToken = default) 
        {
            var followers = await _dataContext.Follows.Where(f => f.FolloweeId == userId).ToListAsync(cancellationToken);

            return followers;
        }

        public async Task<IEnumerable<Follow>> GetFolloweesForUserAsync(Guid userId, 
            CancellationToken cancellationToken = default)
        {
            var followees = await _dataContext.Follows.Where(f => f.FollowerId == userId).ToListAsync(cancellationToken);

            return followees;
        }
    }
}