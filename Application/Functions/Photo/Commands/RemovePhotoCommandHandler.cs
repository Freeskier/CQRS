using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Repos;
using Application.Services;
using Domain.Exceptions;
using MediatR;
using PhotoEntity = Domain.Entities.Photo;

namespace Application.Functions.Photo.Commands
{
    public class RemovePhotoCommandHandler : IRequestHandler<RemovePhotoCommand, Unit>
    {
        private readonly IPhotoRepo _photoRepo;
        private readonly ICloudinaryService _cloudinarySerivce;

        public RemovePhotoCommandHandler(IPhotoRepo photoRepo, ICloudinaryService cloudinaryService)
        {
            _photoRepo = photoRepo ?? throw new ArgumentNullException(nameof(photoRepo));
            _cloudinarySerivce = cloudinaryService ?? throw new ArgumentNullException(nameof(cloudinaryService));
        }

        public async Task<Unit> Handle(RemovePhotoCommand request, CancellationToken cancellationToken)
        {
            
            var photoFromRepo = await GetPhotoAsync(request.PhotoId, request.LoggedUserId);

            await _cloudinarySerivce.RemoveAsync(request.PhotoId);

            var result = await RemoveFromDatabaseAsync(photoFromRepo);

            return result;
        }

        private async Task<PhotoEntity> GetPhotoAsync(Guid photoId, Guid loggedUserId)
        {
            var photoFromRepo = await _photoRepo.GetSinglePhotoAsync(photoId);

            if (photoFromRepo.UserId != loggedUserId)
            {
                throw new OperationForbiddenException("You're not owner of this photo.");
            }

            return photoFromRepo;
        }

        private async Task<Unit> RemoveFromDatabaseAsync(PhotoEntity photo)
        {
            await _photoRepo.RemoveAsync(photo);

            if (await _photoRepo.SaveAllAsync())
            {
                return await Task.FromResult(Unit.Value);
            }

            throw new DatabaseException("Error occured while updating database.");
        }

    }
}