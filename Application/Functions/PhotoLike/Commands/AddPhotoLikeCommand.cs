using Domain.Dtos;
using MediatR;

namespace Application.Functions.PhotoLike.Commands
{
    public class AddPhotoLikeCommand : IRequest
    {
        public PhotoLikeRequest Like { get; set; }
    }
}