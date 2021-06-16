using Domain.Dtos;
using MediatR;


namespace Application.Functions.Follow.Queries
{
    public class GetSingleFollowQuery : IRequest<IsTrueResponse>
    {
        public FollowRequest Follow { get; set; }
    }
}