using System;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.Follow.Commands
{
    public class RemoveFollowCommand : IRequest
    {
        public FollowRequest Follow { get; set; }
    }
}