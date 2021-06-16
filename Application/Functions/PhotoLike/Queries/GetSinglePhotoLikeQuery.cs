using Domain.Dtos;
using MediatR;

namespace Application.Functions.PhotoLike.Queries
{
    public class GetSinglePhotoLikeQuery : IRequest<IsTrueResponse>
    {
        public PhotoLikeRequest Like { get; set; }
    }
}