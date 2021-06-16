using System;
using System.Threading;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.User.Commands
{
    public class AddUserCommand : IRequest
    {
        public UserToRegisterRequest User { get; set; }
    }
}