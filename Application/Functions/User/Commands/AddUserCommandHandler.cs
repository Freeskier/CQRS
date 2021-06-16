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
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Unit>
    {
        private readonly IUserRepo _usersRepo;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public AddUserCommandHandler(IUserRepo usersRepo, IAuthenticationService authenticationService, IMapper mapper)
        {
            _usersRepo = usersRepo ?? throw new ArgumentNullException(nameof(usersRepo));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {          
            await ValidateEmailAndUsername(request.User.UserName, request.User.Email);            

            var mappedUser = MapUser(request.User);

            CreatePassword(ref mappedUser, request.User.Password);

            var result = await AddToDatabaseAsync(mappedUser, cancellationToken);

            return result;
        }

        private async Task ValidateEmailAndUsername(string userName, string email)
        {
            var isUserNameTaken = await IsUserNameTakenAsync(userName);
            var isEmailTaken = IsEmailTakenAsync(email);

            if (isUserNameTaken) 
            {
                throw new OperationForbiddenException("Username is already taken.");
            }

            if (await isEmailTaken)
            {
                throw new OperationForbiddenException("Email is already taken.");
            }
        }

        private AppUser MapUser(UserToRegisterRequest user)
        {
            AppUser mappedUser;

            _mapper.MapUserToRegisterToAppUserEntity(user, out mappedUser);

            return mappedUser;
        }

        private void CreatePassword(ref AppUser user, string password)
        {
            byte[] passwordHash, passwordSalt;

            _authenticationService
                .CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }

        private async Task<Unit> AddToDatabaseAsync(AppUser user, CancellationToken cancellationToken)
        {
            await _usersRepo.AddAsync(user, cancellationToken);

            if (await _usersRepo.SaveAllAsync()) 
            {
                return await Task.FromResult(Unit.Value);   
            }

            throw new DatabaseException("Error occured while updating database.");
        }

        private async Task<bool> IsUserNameTakenAsync(string userName)
        {
            var userNameUser = await _usersRepo.GetSingleUserByUsernameAsync(userName.ToLower());

            return userNameUser is not null;
        }

        private async Task<bool> IsEmailTakenAsync(string email) 
        {
            var emailUser = await _usersRepo.GetSingleUserByEmailAsync(email.ToLower());

            return emailUser is not null;
        }
    }
}