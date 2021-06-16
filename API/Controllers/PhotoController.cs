using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Functions.Photo.Commands;
using Application.Functions.Photo.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Helpers;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")] 
    public class PhotoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto([FromForm]PhotoToAddRequest photo, CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();
            
            var command = new AddPhotoCommand
            {
                Photo = photo,
                UserId = identifier
            };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpGet("followees")]
        public async Task<IActionResult> GetFolloweesPhotos(CancellationToken cancellationToken)
        {
            var identifier = User.GetUserId();

            var query = new GetFolloweesPhotosQuery 
            {
                UserId = identifier
            };

            var photos = await _mediator.Send(query, cancellationToken);

            return Ok(photos);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserPhotos(string id, CancellationToken cancellationToken)
        {
            var identifier = Guid.Parse(id);

            var query = new GetPhotosForUserQuery 
            {
                UserId = identifier
            };

            var photos = await _mediator.Send(query, cancellationToken);

            return Ok(photos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(string id)
        {
            var photoId = Guid.Parse(id);
            var identifier = User.GetUserId();

            var command = new RemovePhotoCommand
            {
                PhotoId = photoId,
                LoggedUserId = identifier
            };

            await _mediator.Send(command);

            return NoContent();
        }


    }
}