using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Functions.Follow.Commands;
using Application.Functions.Follow.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Helpers;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FollowController : ControllerBase
    {
        public IMediator _mediator;
        public FollowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("follow/{userId}")]
        public async Task<IActionResult> FollowUser(string userId, CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();

            var command = new AddFollowCommand
            {
                Follow = new FollowRequest
                {
                    FolloweeId = Guid.Parse(userId),
                    FollowerId = identifier
                }
            };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("unfollow")]
        public async Task<IActionResult> UnfollowUser([FromQuery] GuidRequest followee, CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();

            var follow = new FollowRequest
            {
                FollowerId = identifier,
                FolloweeId = followee.Id
            };

            var command = new RemoveFollowCommand
            {
                Follow = follow
            };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpGet("followers")]
        public async Task<IActionResult> GetFollowers([FromQuery] GuidRequest profile, CancellationToken cancellationToken)
        {
            var command = new GetFollowersQuery
            {
                User = profile
            };

            var followers = await _mediator.Send(command, cancellationToken);

            return Ok(followers);
        }

        [HttpGet("followees")]
        public async Task<IActionResult> GetFollowees([FromQuery] GuidRequest profile, CancellationToken cancellationToken)
        {
            var command = new GetFolloweesQuery
            {
                User = profile
            };

            var followees = await _mediator.Send(command, cancellationToken);

            return Ok(followees);
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetSingleFollow([FromQuery] FollowRequest followRequest, CancellationToken cancellationToken)
        {
            var command = new GetSingleFollowQuery
            {
                Follow = followRequest
            };

            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }
    }
}