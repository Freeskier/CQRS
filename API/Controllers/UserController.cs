using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Functions.User.Commands;
using Application.Functions.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Helpers;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")] 
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> AddUser([FromBody]UserToRegisterRequest user, 
            CancellationToken cancellationToken)
        {
            var command = new AddUserCommand 
            {
                User = user
            };
            
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody]UserToLoginRequest user, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand
            {
                User = user
            };

            var loggedUser = await _mediator.Send(command, cancellationToken);

            return Ok(loggedUser);
        }

        [HttpPut("main/update")]
        public async Task<IActionResult> UpdateMainPhoto([FromForm]MainPhotoToUpdateRequest photo, CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();

            var command = new UpdateMainPhotoCommand
            {
                Photo = photo,
                UserId = identifier
            };

            var updatedUser = await _mediator.Send(command, cancellationToken);

            return Ok(updatedUser);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetUsersWithKeyWord([FromQuery]GetUsersWithKeyWordQuery keyWordQuery, CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(keyWordQuery, cancellationToken);

            return Ok(users);
        }

        [HttpPut("profile/update")]
        public async Task<IActionResult> UpdateProfile([FromBody]UserToUpdateRequest user, CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();

            var command = new UpdateProfileCommand
            {
                User = user,
                UserId = identifier
            };

            var updatedUser = await _mediator.Send(command, cancellationToken);

            return Ok(updatedUser);
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetSingleUser([FromQuery]GuidRequest userIdRequest, CancellationToken cancellationToken)
        {
            var command = new GetSingleUserQuery
            {
                User = userIdRequest
            };

            var user = await _mediator.Send(command, cancellationToken);

            return Ok(user);
        }
    }
}