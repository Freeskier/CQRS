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
    public class AddPhotoLikeCommandHandler : IRequestHandler<AddPhotoLikeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IPhotoLikeRepo _photoLikeRepo;
        private readonly IPhotoRepo _photoRepo;
        public AddPhotoLikeCommandHandler(IMapper mapper, IPhotoLikeRepo photoLikeRepo, IPhotoRepo photoRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _photoLikeRepo = photoLikeRepo ?? throw new ArgumentNullException(nameof(photoLikeRepo));
            _photoRepo = photoRepo ?? throw new ArgumentNullException(nameof(photoRepo));
        }
        public async Task<Unit> Handle(AddPhotoLikeCommand request, CancellationToken cancellationToken)
        {
            await ValidateAsync(request.Like);

            var result = await AddToDatabaseAsync(request.Like, cancellationToken);

            return result;

        }

        private async Task<IAsyncResult> ValidateAsync(PhotoLikeRequest request)
        {
            var isAlreadyLiked = await IsAlreadyLikedAsync(request);

            var doesPhotoExistTask = DoesPhotoExistsAsync(request);

            if (isAlreadyLiked)
            {
                throw new DatabaseException("You've already liked this photo.");
            }

            if (await doesPhotoExistTask)
            {
                return Task.CompletedTask;
            }

            throw new NotFoundException("Photo you wanted to like, doesn't exist.");

        }

        private async Task<bool> IsAlreadyLikedAsync(PhotoLikeRequest request)
        {
            var likeFromRepo = await _photoLikeRepo.GetSinglePhotoLikeAsync(request.PhotoId, request.UserId);

            if (likeFromRepo is not null)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> DoesPhotoExistsAsync(PhotoLikeRequest request)
        {
            var photoFromRepo = await _photoRepo.GetSinglePhotoAsync(request.PhotoId);

            if (photoFromRepo is not null)
            {
                return true;
            }

            return false;
        }

        private async Task<Unit> AddToDatabaseAsync(PhotoLikeRequest request, CancellationToken cancellationToken)
        {
            PhotoLikeEntity photoLike;

            _mapper.MapPhotoLikeRequestToPhotoLikeEntity(request, out photoLike);

            await _photoLikeRepo.AddAsync(photoLike, cancellationToken);

            if (await _photoLikeRepo.SaveAllAsync())
            {
                return await Task.FromResult(Unit.Value);
            }

            throw new DatabaseException("Error occured while updating database");
        }

    }
}