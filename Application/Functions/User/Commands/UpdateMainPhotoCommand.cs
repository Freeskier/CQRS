using System;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Functions.User.Commands
{
    public class UpdateMainPhotoCommand : IRequest<UserDetailResponse>
    {
        public Guid UserId { get; set; }
        public MainPhotoToUpdateRequest Photo { get; set; }
    }
}