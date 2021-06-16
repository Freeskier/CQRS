using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Repos;
using Domain.Exceptions;
using MediatR;
using CommentEntity = Domain.Entities.Comment;

namespace Application.Functions.Comment.Commands
{
    public class RemoveCommentCommandHandler : IRequestHandler<RemoveCommentCommand, Unit>
    {
        private readonly ICommentRepo _commentRepo;
        public RemoveCommentCommandHandler(ICommentRepo commentRepo)
        {
            _commentRepo = commentRepo ?? throw new ArgumentNullException(nameof(commentRepo));
        }
        public async Task<Unit> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            var commentFromRepo = await GetCommentAsync(request.Comment.Id);

            CheckIfCanRemove(commentFromRepo, request.LoggedUserId);

            var result = await RemoveFromDatabseAsync(commentFromRepo);

            return result;
        }

        private async Task<CommentEntity> GetCommentAsync(Guid commentId)
        {
            var commentFromRepo = await _commentRepo.GetSingleAsync(commentId);

            if (commentFromRepo is null)
            {
                throw new NotFoundException("Comment not found.");
            }

            return commentFromRepo;
        }

        private void CheckIfCanRemove(CommentEntity comment, Guid loggedUser)
        {
            if (comment.UserId != loggedUser)
            {
                throw new OperationForbiddenException("You're not owner of this comment.");
            }
        }

        private async Task<Unit> RemoveFromDatabseAsync(CommentEntity comment)
        {
            await _commentRepo.RemoveAsync(comment);

            if(await _commentRepo.SaveAllAsync())
            {
                return await Task.FromResult(Unit.Value);
            }

            throw new DatabaseException("Error while removing comment.");
        }
    }
}