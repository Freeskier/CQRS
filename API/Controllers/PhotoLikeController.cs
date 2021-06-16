using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Functions.PhotoLike.Commands;
using Application.Functions.PhotoLike.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Helpers;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PhotoLikeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhotoLikeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody]GuidRequest request, CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();

            var command = new AddPhotoLikeCommand
            {
                Like = new PhotoLikeRequest 
                {
                    PhotoId = request.Id,
                    UserId = identifier
                }
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLike([FromQuery]GuidRequest request, CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();

            var command = new DeletePhotoLikeCommand
            {
                Like = new PhotoLikeRequest 
                {
                    PhotoId = request.Id,
                    UserId = identifier
                }
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{photoId}")]
        public async Task<IActionResult> GetLikesForPhoto(Guid photoId, CancellationToken cancellationToken)
        {
            var query = new GetPhotoLikesForPhotoQuery
            {
                Photo = new GuidRequest 
                {
                    Id = photoId
                }
            };

            var likers = await _mediator.Send(query);

            return Ok(likers);
        }

        [HttpGet("isLiked")]
        public async Task<IActionResult> GetSingleLike([FromQuery]PhotoLikeRequest photoLike, CancellationToken cancellationToken)
        {
            var query = new GetSinglePhotoLikeQuery
            {
                Like = photoLike
            };

            var doesExist = await _mediator.Send(query);

            return Ok(doesExist);
        }
    }
}