using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using MediatR;

namespace Application.Functions.Photo.Queries
{
    public class GetFolloweesPhotosQueryHandler : IRequestHandler<GetFolloweesPhotosQuery, IEnumerable<PhotoToReturnResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IPhotoRepo _photoRepo;

        public GetFolloweesPhotosQueryHandler(IMapper mapper, IPhotoRepo photoRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _photoRepo = photoRepo ?? throw new ArgumentNullException(nameof(photoRepo));
        }
        
        public async Task<IEnumerable<PhotoToReturnResponse>> Handle(GetFolloweesPhotosQuery request, CancellationToken cancellationToken)
        {

            var photosFromRepo = await _photoRepo.GetPhotosForFolloweesAsync(request.UserId);

            IEnumerable<PhotoToReturnResponse> photosToReturn;

            _mapper.MapPhotoEntityToPhotoToReturnResponse(photosFromRepo, out photosToReturn);

            return photosToReturn;
        }
    }
}