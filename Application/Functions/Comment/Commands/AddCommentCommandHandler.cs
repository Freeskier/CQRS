using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using Domain.Exceptions;
using MediatR;
using CommentEntity = Domain.Entities.Comment;
using PhotoEntity = Domain.Entities.Photo;

namespace Application.Functions.Comment.Commands
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Unit>
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IPhotoRepo _photoRepo;
        private readonly IMapper _mapper;

        public AddCommentCommandHandler(ICommentRepo commentRepo, IMapper mapper, IPhotoRepo photoRepo)
        {
            _commentRepo = commentRepo ?? throw new ArgumentNullException(nameof(commentRepo));
            _photoRepo = photoRepo ?? throw new ArgumentNullException(nameof(photoRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            await CheckIfPhotoExist(request.Comment.PhotoId);

            var commentToAdd = MapRequestToEntity(request.Comment, request.LoggedUserId);

            var result = await AddToDatabaseAsync(commentToAdd, cancellationToken);

            return result;

        }

        private async Task CheckIfPhotoExist(Guid photoId)
        {
            var photo = await _photoRepo.GetSinglePhotoAsync(photoId);

            if (photo is null)
            {
                throw new NotFoundException("Photo doesn't exist.");
            }
        }

        private CommentEntity MapRequestToEntity(CommentToAddRequest comment, Guid userId)
        {
            var commentEntity = new CommentEntity();

            _mapper.MapCommentToAddRequestToCommentEntity(comment, out commentEntity);

            commentEntity.UserId = userId;

            return commentEntity;
        }

        private async Task<Unit> AddToDatabaseAsync(CommentEntity comment, CancellationToken cancellationToken)
        {
            await _commentRepo.AddAsync(comment, cancellationToken);

            if (await _commentRepo.SaveAllAsync())
            {
                return await Task.FromResult(Unit.Value);
            }

            throw new DatabaseException("Error occured while adding comment.");
        }
    }
}