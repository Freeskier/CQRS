using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


namespace Infrastructure.Services
{
    
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig ?? throw new ArgumentNullException(nameof(cloudinaryConfig)); ;

            var account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<IAsyncResult> RemoveAsync(Guid photoId)
        {
            var deletionParams = new DeletionParams(photoId.ToString());

            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result == "ok")
            {
                return Task.CompletedTask;
            }

            throw new Exception(result.Error.Message);
        }

        public async Task<(string, string)> AddPhotoWithRandomGuidAsync(IFormFile file, CancellationToken cancellationToken = default(CancellationToken))
        {
            var id = Guid.NewGuid();

            var uploadResult = await AddPhotoToCloudinary(file, id, cancellationToken);

            return GetUrlAndPublicId(uploadResult);
        }

        public async Task<string> AddPhotoWithCustomGuidAsync(IFormFile file, Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uploadResult = await AddPhotoToCloudinary(file, id, cancellationToken);

            return GetUrl(uploadResult);
        }

        private async Task<ImageUploadResult> AddPhotoToCloudinary(IFormFile file, Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (file.Length <= 0) throw new ArgumentNullException("Cannot add an empty file.");

            var uploadResult = new ImageUploadResult();

            var publicId = id.ToString();

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation()
                        .Width(500)
                        .Height(500)
                        .Crop("fill")
                        .Gravity("face"),
                    PublicId = publicId
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);
            }

            return uploadResult;
        }

        private string GetUrl(ImageUploadResult uploadResult)
        {
            return uploadResult.Url.ToString();
        }

        private (string, string) GetUrlAndPublicId(ImageUploadResult uploadResult)
        {
            return (uploadResult.Url.ToString(), uploadResult.PublicId);
        }

    }
}