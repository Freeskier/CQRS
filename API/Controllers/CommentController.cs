using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Functions.Comment.Commands;
using Application.Functions.Comment.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Helpers;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody]CommentToAddRequest comment, CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();
            
            var command = new AddCommentCommand
            {
                Comment = comment,
                LoggedUserId = identifier
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveComment([FromQuery]GuidRequest commentId)
        {
            var identifier = User.GetUserId();

            var command = new RemoveCommentCommand
            {
                Comment = commentId,
                LoggedUserId = identifier
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{photoId}")]
        public async Task<IActionResult> GetCommentsForPhoto(string photoId)
        {
            var query = new GetCommentsForPhotoQuery
            {
                Photo = new GuidRequest
                {
                    Id = Guid.Parse(photoId)
                }
            };

            var comments = await _mediator.Send(query);

            return Ok(comments);
        }

    }
}