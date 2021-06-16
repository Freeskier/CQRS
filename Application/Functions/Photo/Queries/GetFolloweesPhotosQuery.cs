using System;
using System.Collections.Generic;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.Photo.Queries
{
    public class GetFolloweesPhotosQuery : IRequest<IEnumerable<PhotoToReturnResponse>>
    {
        public Guid UserId { get; set; }
    }
}