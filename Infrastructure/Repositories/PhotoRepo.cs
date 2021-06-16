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
    
    public class PhotoRepo : GenericRepo<Photo>, IPhotoRepo
    {
        public PhotoRepo(ApplicationDataContext dataContext) : base(dataContext) { }

        public async Task<Photo> GetSinglePhotoAsync(Guid photoId)
        {
            var photo = await _dataContext.Photos.FindAsync(photoId);

            return photo;
        }

        public async Task<IEnumerable<Photo>> GetPhotosForUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var photos = await _dataContext.Photos
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.DateAdded)
                .ToListAsync(cancellationToken);

            return photos;
        }

        public async Task<IEnumerable<Photo>> GetPhotosForFolloweesAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var photos = await _dataContext.Photos
                .Where(p => p.User.Followers.Any(x => x.FollowerId == userId))
                .OrderByDescending(p => p.DateAdded)
                .ToListAsync(cancellationToken);

            return photos;
        }
        
    }
}