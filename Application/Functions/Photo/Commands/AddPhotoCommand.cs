using System;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.Photo.Commands
{
    public class AddPhotoCommand : IRequest
    {
        public Guid UserId { get; set; }
        public PhotoToAddRequest Photo { get; set; }
    }
}