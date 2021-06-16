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
    public class GetFolloweesQueryHandler : IRequestHandler<GetFolloweesQuery, IEnumerable<UserDetailResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IFollowRepo _followRepo;

        public GetFolloweesQueryHandler(IMapper mapper, IFollowRepo followRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _followRepo = followRepo ?? throw new ArgumentNullException(nameof(followRepo));
        }
        public async Task<IEnumerable<UserDetailResponse>> Handle(GetFolloweesQuery request, CancellationToken cancellationToken)
        {
            var followeesFromRepo = await GetFolloweesAsync(request.User.Id, cancellationToken);

            var mappedFollowees = MapFollowees(followeesFromRepo);

            return mappedFollowees; 
            
        }

        private async Task<IEnumerable<FollowEntity>> GetFolloweesAsync(Guid userId, CancellationToken cancellationToken)
        {
            var followees = await _followRepo.GetFolloweesForUserAsync(userId, cancellationToken);

            if (followees is null) throw new NotFoundException();

            return followees;
        }

        private IEnumerable<UserDetailResponse> MapFollowees(IEnumerable<FollowEntity> followeesFromRepo)
        {
            IEnumerable<UserDetailResponse> followeesToReturn;

            _mapper.MapFollowEntityToFolloweeDetailResponse(followeesFromRepo, out followeesToReturn);

            return followeesToReturn;
        }
    }
}