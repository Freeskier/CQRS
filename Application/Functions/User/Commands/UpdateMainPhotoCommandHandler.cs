using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using Application.Services;
using Domain.Exceptions;
using Domain.Entities;
using MediatR;


namespace Application.Functions.User.Commands
{
    public class UpdateMainPhotoCommandHandler : IRequestHandler<UpdateMainPhotoCommand, UserDetailResponse>
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public UpdateMainPhotoCommandHandler(IUserRepo userRepo, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _cloudinaryService = cloudinaryService ?? throw new ArgumentNullException(nameof(cloudinaryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDetailResponse> Handle(UpdateMainPhotoCommand request, CancellationToken cancellationToken)
        {
            var mainPhotoUrl = await _cloudinaryService.AddPhotoWithCustomGuidAsync(request.Photo.File, request.UserId, cancellationToken);

            var updatedUser = await UpdateAndReturnUserAsync(mainPhotoUrl, request.UserId, cancellationToken);

            var userToReturn = MapAndReturn(updatedUser);

            return userToReturn;
        }

        private async Task<AppUser> UpdateAndReturnUserAsync(string photoUrl, Guid userId, CancellationToken cancellationToken)
        {
            var userFromRepo = await _userRepo.GetSingleUserByIdAsync(userId, cancellationToken);

            userFromRepo.MainPhotoUrl = photoUrl;

            await _userRepo.UpdateAsync(userFromRepo);

            if (await _userRepo.SaveAllAsync())
            {
                return userFromRepo;
            }

            throw new DatabaseException("Error occured while updating database.");
        }

        private UserDetailResponse MapAndReturn(AppUser user)
        {
            UserDetailResponse userToReturn;

            _mapper.MapAppUserEntityToUserDetailResponse(user, out userToReturn);

            return userToReturn;
        }

        
    }
}