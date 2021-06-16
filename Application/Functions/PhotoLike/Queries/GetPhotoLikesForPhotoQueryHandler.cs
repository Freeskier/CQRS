using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using MediatR;
using PhotoLikeEntity = Domain.Entities.PhotoLike;

namespace Application.Functions.PhotoLike.Queries
{
    public class GetPhotoLikesForPhotoQueryHandler : IRequestHandler<GetPhotoLikesForPhotoQuery, IEnumerable<LikerResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPhotoLikeRepo _photoLikeRepo;
        public GetPhotoLikesForPhotoQueryHandler(IMapper mapper, IPhotoLikeRepo photoLikeRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _photoLikeRepo = photoLikeRepo ?? throw new ArgumentNullException(nameof(photoLikeRepo));
        }

        public async Task<IEnumerable<LikerResponse>> Handle(GetPhotoLikesForPhotoQuery request, CancellationToken cancellationToken)
        {
            var likes = await GetPhotoLikesFromDatabaseAsync(request.Photo);

            var mappedLikers = MapLikes(likes);

            return mappedLikers;
        }

        private async Task<IEnumerable<PhotoLikeEntity>> GetPhotoLikesFromDatabaseAsync(GuidRequest photo)
        {
            var likesFromDatabase = await _photoLikeRepo.GetLikesForPhotoAsync(photo.Id);

            return likesFromDatabase;
        }

        private IEnumerable<LikerResponse> MapLikes(IEnumerable<PhotoLikeEntity> likes)
        {
            IEnumerable<LikerResponse> likers;

            _mapper.MapPhotoLikeEntityToLikerResponse(likes, out likers);

            return likers;
        }

    }
}