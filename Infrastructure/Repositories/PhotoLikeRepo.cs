using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repos
{
    
    public class PhotoLikeRepo : GenericRepo<PhotoLike>, IPhotoLikeRepo
    {
        public PhotoLikeRepo(ApplicationDataContext dataContext) : base(dataContext) { }

        public async Task<PhotoLike> GetSinglePhotoLikeAsync(Guid photoId, Guid userId)
        {
            var like = await _dataContext.PhotoLikes.FindAsync(photoId, userId);

            return like;
        }

        public async Task<IEnumerable<PhotoLike>> GetLikesForPhotoAsync(Guid photoId)
        {
            var likes = await _dataContext
                .PhotoLikes
                .Where(x => x.PhotoId == photoId)
                .ToListAsync();

            return likes;
        }
    }
}