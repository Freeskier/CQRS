using Domain.Dtos;
using MediatR;

namespace Application.Functions.PhotoLike.Commands
{
    public class DeletePhotoLikeCommand : IRequest
    {
        public PhotoLikeRequest Like { get; set; }
    }
}