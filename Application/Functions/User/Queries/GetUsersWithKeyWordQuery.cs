using System.Collections.Generic;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.User.Queries
{
    public class GetUsersWithKeyWordQuery : IRequest<IEnumerable<UserDetailResponse>>
    {
        public string KeyWord { get; set; }
    }
}