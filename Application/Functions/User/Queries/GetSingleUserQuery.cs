using Domain.Dtos;
using MediatR;

namespace Application.Functions.User.Queries
{
    public class GetSingleUserQuery : IRequest<UserDetailResponse>
    {
        public GuidRequest User { get; set; }
    }
}