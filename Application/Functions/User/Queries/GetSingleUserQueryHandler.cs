using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using Domain.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Functions.User.Queries
{
    public class GetSingleUserQueryHandler : IRequestHandler<GetSingleUserQuery, UserDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public GetSingleUserQueryHandler(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));;
        }

        public async Task<UserDetailResponse> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
        {
            var userFromRepo = await GetAppUserAsync(request.User.Id, cancellationToken);

            var userToReturn = MapAndReturn(userFromRepo);

            return userToReturn;
        }

        private async Task<AppUser> GetAppUserAsync(Guid id, CancellationToken cancellationToken)
        {
            var userFromRepo = await _userRepo.GetSingleUserByIdAsync(id, cancellationToken);

            if (userFromRepo is null)
            {
                throw new NotFoundException("User hasn't been found.");
            }

            return userFromRepo;
        }

        private UserDetailResponse MapAndReturn(AppUser user)
        {
            UserDetailResponse userToReturn;

            _mapper.MapAppUserEntityToUserDetailResponse(user, out userToReturn);

            return userToReturn;
        }
    }
}