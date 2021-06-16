using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using Domain.Exceptions;
using MediatR;
using FollowEntity = Domain.Entities.Follow;

namespace Application.Functions.Follow.Queries
{
    public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, IEnumerable<UserDetailResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IFollowRepo _followRepo;

        public GetFollowersQueryHandler(IMapper mapper, IFollowRepo followRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _followRepo = followRepo ?? throw new ArgumentNullException(nameof(followRepo));
        }

        public async Task<IEnumerable<UserDetailResponse>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
        {
            var followersFromRepo = await GetFollowersAsync(request.User.Id, cancellationToken);

            var mappedFollowers = MapFollowers(followersFromRepo);

            return mappedFollowers;
        }

        private async Task<IEnumerable<FollowEntity>> GetFollowersAsync(Guid userId, CancellationToken cancellationToken)
        {
            var followers = await _followRepo.GetFollowersForUserAsync(userId, cancellationToken);

            if (followers is null) throw new NotFoundException();

            return followers;
        }

        private IEnumerable<UserDetailResponse> MapFollowers(IEnumerable<FollowEntity> followersFromRepo)
        {
            IEnumerable<UserDetailResponse> followersToReturn;

            _mapper.MapFollowEntityToFollowerDetailResponse(followersFromRepo, out followersToReturn);

            return followersToReturn;
        }
    }
}