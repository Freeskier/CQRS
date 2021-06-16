using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using Domain.Exceptions;
using MediatR;
using PhotoLikeEntity = Domain.Entities.PhotoLike;

namespace Application.Functions.PhotoLike.Commands
{
    public class DeletePhotoLikeCommandHandler : IRequestHandler<DeletePhotoLikeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IPhotoLikeRepo _photoLikeRepo;
        private readonly IPhotoRepo _photoRepo;
        public DeletePhotoLikeCommandHandler(IMapper mapper, IPhotoLikeRepo photoLikeRepo, IPhotoRepo photoRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _photoLikeRepo = photoLikeRepo ?? throw new ArgumentNullException(nameof(photoLikeRepo));
            _photoRepo = photoRepo ?? throw new ArgumentNullException(nameof(photoRepo));
        }
        public async Task<Unit> Handle(DeletePhotoLikeCommand request, CancellationToken cancellationToken)
        {
            await CheckIfPhotoExistAsync(request.Like);

            var likeFromRepo = await GetLikeFromDatabaseAsync(request.Like);

            return await RemoveFromDatabaseAsync(likeFromRepo);
        }

        private async Task<IAsyncResult> CheckIfPhotoExistAsync(PhotoLikeRequest request)
        {
            var photoFromRepo = await _photoRepo.GetSinglePhotoAsync(request.PhotoId);

            if (photoFromRepo is not null)
            {
                return Task.CompletedTask;
            }

            throw new NotFoundException("Photo you wanted to unlike doesn't exist.");
        }

        private async Task<PhotoLikeEntity> GetLikeFromDatabaseAsync(PhotoLikeRequest request)
        {
            var likeFromRepo = await _photoLikeRepo.GetSinglePhotoLikeAsync(request.PhotoId, request.UserId);

            if (likeFromRepo is not null)
            {
                return likeFromRepo;
            }

            throw new NotFoundException("You haven't liked this photo.");
        }

        

        private async Task<Unit> RemoveFromDatabaseAsync(PhotoLikeEntity entity)
        {
            await _photoLikeRepo.RemoveAsync(entity);

            if(await _photoLikeRepo.SaveAllAsync())
            {
                return await Task.FromResult(Unit.Value);
            }

            throw new DatabaseException("Error occured while updating database.");
        }
    }
}