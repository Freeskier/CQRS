using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using Domain.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Functions.User.Commands
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UserDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        public UpdateProfileCommandHandler(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }
 
        public async Task<UserDetailResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var userFromRepo = await GetUserAsync(request.UserId, cancellationToken);

            var updatedUser = await MapAndUpdateAsync(userEntity: userFromRepo, userToUpdate: request.User);

            var result = MapToDetailAndReturn(updatedUser);

            return result;
        }

        private async Task<AppUser> GetUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var userFromRepo = await _userRepo.GetSingleUserByIdAsync(id, cancellationToken);

            return userFromRepo;
        }

        private async Task<AppUser> MapAndUpdateAsync(AppUser userEntity, UserToUpdateRequest userToUpdate)
        {
             _mapper.MapUserToUpdateRequestToAppUserEntity(userToUpdate, ref userEntity);

            await _userRepo.UpdateAsync(userEntity);

            if (!await _userRepo.SaveAllAsync())
            {
                throw new DatabaseException("Error occured while updating database.");
            }

            var userToReturn = new UserDetailResponse();

            return userEntity;
        }

        private UserDetailResponse MapToDetailAndReturn(AppUser user)
        {
            UserDetailResponse userDetail;

            _mapper.MapAppUserEntityToUserDetailResponse(user, out userDetail);

            return userDetail;
        }
    }
}