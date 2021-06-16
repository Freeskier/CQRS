using System;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.Comment.Commands
{
    public class RemoveCommentCommand : IRequest
    {
        public GuidRequest Comment { get; set; }
        public Guid LoggedUserId { get; set; }
    }
}