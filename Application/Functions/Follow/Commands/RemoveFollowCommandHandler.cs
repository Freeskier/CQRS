using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Mapper;
using Application.Repos;
using Domain.Exceptions;
using MediatR;
using FollowEntity = Domain.Entities.Follow;

namespace Application.Functions.Follow.Commands
{
    public class RemoveFollowCommandHandler : IRequestHandler<RemoveFollowCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IFollowRepo _followRepo;

        public RemoveFollowCommandHandler(IFollowRepo followRepo, IMapper mapper)
        {
            _followRepo = followRepo ?? throw new ArgumentNullException(nameof(followRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(RemoveFollowCommand request, CancellationToken cancellationToken)
        {
            var loggedUserId = request.Follow.FollowerId;
            var followeeId = request.Follow.FolloweeId;

            var followFromRepo = await GetFollowAsync(loggedUserId, followeeId);

            var result = await RemoveFollowFromDatabaseAsync(followFromRepo);

            return result;
        }

        private async Task<FollowEntity> GetFollowAsync(Guid loggedUserId, Guid followeeId)
        {
            var followFromRepo = 
                await _followRepo.GetSingleFollowAsync(loggedUserId, 
                followeeId);

            if (followFromRepo is null) throw new NotFoundException("Follow hasn't been found.");

            return followFromRepo;
        }

        private async Task<Unit> RemoveFollowFromDatabaseAsync(FollowEntity follow)
        {
            await _followRepo.RemoveAsync(follow);
            
            if (await _followRepo.SaveAllAsync())
            {
                return await Task.FromResult(Unit.Value);  
            }

            throw new DatabaseException("Error occured while updating database.");
        }
    }
}