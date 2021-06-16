using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Repos;
using MediatR;
using PhotoLikeEntity = Domain.Entities.PhotoLike;

namespace Application.Functions.PhotoLike.Queries
{
    public class GetSinglePhotoLikeQueryHandler : IRequestHandler<GetSinglePhotoLikeQuery, IsTrueResponse>
    {
        private readonly IPhotoLikeRepo _photoLikeRepo;

        public GetSinglePhotoLikeQueryHandler(IPhotoLikeRepo photoLikeRepo)
        {
            _photoLikeRepo = photoLikeRepo ?? throw new ArgumentNullException(nameof(photoLikeRepo));
        }

        public async Task<IsTrueResponse> Handle(GetSinglePhotoLikeQuery request, CancellationToken cancellationToken)
        {
            var likeFromRepo = await GetFromDatabase(request.Like);

            var doesExist = CheckIfExistAndReturn(likeFromRepo);

            return doesExist;
        }

        private async Task<PhotoLikeEntity> GetFromDatabase(PhotoLikeRequest like) 
        {
            var likeFromRepo = await _photoLikeRepo.GetSinglePhotoLikeAsync(like.PhotoId, like.UserId);

            return likeFromRepo;
        }

        private IsTrueResponse CheckIfExistAndReturn(PhotoLikeEntity likeEntity)
        {
            var isTrueResponse = new IsTrueResponse();

            if (likeEntity is not null)
            {
                isTrueResponse.IsTrue = true;
            }

            return isTrueResponse;
        }
    }
}