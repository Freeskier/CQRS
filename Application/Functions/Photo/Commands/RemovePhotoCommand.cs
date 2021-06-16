using System;
using MediatR;

namespace Application.Functions.Photo.Commands
{
    public class RemovePhotoCommand : IRequest
    {
        public Guid PhotoId { get; set; }
        public Guid LoggedUserId { get; set; }
    }
}