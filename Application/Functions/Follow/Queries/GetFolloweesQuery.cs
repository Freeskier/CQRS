using System.Collections.Generic;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.Follow.Queries
{
    public class GetFolloweesQuery : IRequest<IEnumerable<UserDetailResponse>>
    {
        public GuidRequest User { get; set; }
    }
}