using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repos
{
    public interface IPhotoRepo : IGenericRepo<Photo>
    {
        Task<IEnumerable<Photo>> GetPhotosForFolloweesAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Photo>> GetPhotosForUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Photo> GetSinglePhotoAsync(Guid photoId);
    }
}