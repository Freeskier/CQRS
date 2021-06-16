using System.Collections.Generic;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.PhotoLike.Queries
{
    public class GetPhotoLikesForPhotoQuery : IRequest<IEnumerable<LikerResponse>>
    {
        public GuidRequest Photo { get; set; }
    }
}