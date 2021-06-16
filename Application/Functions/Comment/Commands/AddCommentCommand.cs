using System;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.Comment.Commands
{
    public class AddCommentCommand : IRequest
    {
        public CommentToAddRequest Comment { get; set; }
        public Guid LoggedUserId { get; set; }
    }
}