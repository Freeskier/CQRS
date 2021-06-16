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
    public class AddFollowCommandHandler : IRequestHandler<AddFollowCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IFollowRepo _followRepo;
        private readonly IUserRepo _userRepo;

        public AddFollowCommandHandler(IMapper mapper, IFollowRepo followRepo, IUserRepo userRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _followRepo = followRepo ?? throw new ArgumentNullException(nameof(followRepo));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }
        public async Task<Unit> Handle(AddFollowCommand request, CancellationToken cancellationToken)
        {
            var followeeId = request.Follow.FolloweeId;
            var followerId = request.Follow.FollowerId;
            
            var follow = CheckSelffollowConditionAndReturnEntity(followerId, followeeId);
            
            await ValidateAsync(follow);

            var result = await AddToDatabaseAsync(follow, cancellationToken);

            return result;
            
        }

        private FollowEntity CheckSelffollowConditionAndReturnEntity(Guid followerId, Guid followeeId)
        {
            if (followerId == followeeId)
            {
                throw new OperationForbiddenException("You cannot follow yourself.");
            }

            var follow = new FollowEntity
            {
                FolloweeId = followeeId,
                FollowerId = followerId
            };

            return follow;
        }

        private async Task ValidateAsync(FollowEntity follow)
        {
            if (!await DoesFolloweExistAsync(follow))
            {
                throw new NotFoundException("User doesn't exist.");
            }

            if (await IsAlreadyFollowedAsync(follow))
            {
                throw new DatabaseException("You've already followed this user.");
            }   
        }

        private async Task<Unit> AddToDatabaseAsync(FollowEntity follow, CancellationToken cancellationToken)
        {
            await _followRepo.AddAsync(follow, cancellationToken);

            if (await _followRepo.SaveAllAsync())
            {
                return await Task.FromResult(Unit.Value);  
            }

            throw new DatabaseException("Error occured while updating database.");
        }

        private async Task<bool> DoesFolloweExistAsync(FollowEntity follow)
        {
            var userToBeFollowed = await _userRepo.GetSingleUserByIdAsync(follow.FolloweeId);
            
            return userToBeFollowed is not null;
        }

        private async Task<bool> IsAlreadyFollowedAsync(FollowEntity follow)
        {
            var followFromRepo = await _followRepo.GetSingleFollowAsync(follow.FollowerId, follow.FolloweeId);

            return followFromRepo is not null;
        }
    }
}