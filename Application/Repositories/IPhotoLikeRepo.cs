using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repos
{
    public interface IPhotoLikeRepo : IGenericRepo<PhotoLike>
    {
        Task<IEnumerable<PhotoLike>> GetLikesForPhotoAsync(Guid photoId);
        Task<PhotoLike> GetSinglePhotoLikeAsync(Guid photoId, Guid userId);
    }
}