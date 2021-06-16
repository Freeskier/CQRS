using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Repos;
using MediatR;
using FollowEntity = Domain.Entities.Follow;

namespace Application.Functions.Follow.Queries
{
    public class GetSingleFollowQueryHandler : IRequestHandler<GetSingleFollowQuery, IsTrueResponse>
    {
        private readonly IFollowRepo _followRepo;
        public GetSingleFollowQueryHandler(IFollowRepo followRepo)
        {
            _followRepo = followRepo ?? throw new ArgumentNullException(nameof(followRepo));
        }
        public async Task<IsTrueResponse> Handle(GetSingleFollowQuery request, CancellationToken cancellationToken)
        {
            var followerId = request.Follow.FollowerId;
            var followeeId = request.Follow.FolloweeId;

            var followFromRepo = await GetFollowAsync(followerId, followeeId);

            var isTrue = CheckIfExistAndReturn(followFromRepo);

            return isTrue;
        }

        private async Task<FollowEntity> GetFollowAsync(Guid followerId, Guid followeeId)
        {
            var followFromRepo = await _followRepo
                .GetSingleFollowAsync(followerId: followerId, followeeId: followeeId);

            return followFromRepo;
        }

        private IsTrueResponse CheckIfExistAndReturn(FollowEntity follow)
        {
            var isTrueResponse = new IsTrueResponse();

            if (follow is not null)
            {
                isTrueResponse.IsTrue = true;
            }

            return isTrueResponse;

        }
    }
}