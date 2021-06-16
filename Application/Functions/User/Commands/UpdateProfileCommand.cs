using System;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.User.Commands
{
    public class UpdateProfileCommand : IRequest<UserDetailResponse>
    {
        public Guid UserId { get; set; }
        public UserToUpdateRequest User { get; set; }
    }
}