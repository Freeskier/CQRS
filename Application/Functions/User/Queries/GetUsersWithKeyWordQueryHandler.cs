using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using MediatR;

namespace Application.Functions.User.Queries
{
    public class GetUsersWithKeyWordQueryHandler : IRequestHandler<GetUsersWithKeyWordQuery, IEnumerable<UserDetailResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public GetUsersWithKeyWordQueryHandler(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        public async Task<IEnumerable<UserDetailResponse>> Handle(GetUsersWithKeyWordQuery request, CancellationToken cancellationToken)
        {
            var usersFromRepo = await _userRepo.GetUsersWithKeyWord(request.KeyWord.ToLower(), cancellationToken);

            IEnumerable<UserDetailResponse> usersToReturn;

            _mapper.MapAppUserEntityToUserDetailResponse(usersFromRepo, out usersToReturn);

            return usersToReturn;
        }

    }
}