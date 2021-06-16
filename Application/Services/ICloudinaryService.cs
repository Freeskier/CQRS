using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public interface ICloudinaryService
    {
        Task<string> AddPhotoWithCustomGuidAsync(IFormFile file, Guid id, CancellationToken cancellationToken = default);
        Task<(string, string)> AddPhotoWithRandomGuidAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task<IAsyncResult> RemoveAsync(Guid photoId);
    }
}