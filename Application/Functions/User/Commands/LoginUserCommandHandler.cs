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
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDetailResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IAuthenticationService authenticationService, IUserRepo userRepo, IMapper mapper)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<UserDetailResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userFromRepository = await GetUserAsync(request.User.UserName, cancellationToken);

            VerifyUser(userFromRepository, request.User.Password);
            
            var userToReturn = await CreateTokenAndMapAsync(userFromRepository);

            return userToReturn;
        }

        private async Task<AppUser> GetUserAsync(string userName, CancellationToken cancellationToken)
        {
            var userFromRepository = await _userRepo.GetSingleUserByUsernameAsync(userName, cancellationToken);

            return userFromRepository;
        }

        private void VerifyUser(AppUser user, string password)
        {
            if (user is null) throw new UnauthorizedException("Wrong username or password.");

            if (!_authenticationService.VerifyPasswordHash(password, 
            user.PasswordHash, user.PasswordSalt)) 
            {
                throw new UnauthorizedException("Wrong username or password.");
            }
        }

        private async Task<UserDetailResponse> CreateTokenAndMapAsync(AppUser user)
        {
            var tokenTask = _authenticationService.CreateToken(
                user.Id,
                user.UserName,
                user.Email
            );

            UserDetailResponse userResponse;

            _mapper.MapAppUserEntityToUserDetailResponse(user, out userResponse);

            userResponse.Token = await tokenTask;

            return userResponse;
        }
    }
}