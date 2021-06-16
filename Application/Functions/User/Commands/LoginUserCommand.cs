using Domain.Dtos;
using MediatR;
namespace Application.Functions.User.Commands
{
    public class LoginUserCommand : IRequest<UserDetailResponse>
    {
        public UserToLoginRequest User { get; set; }
    }
}