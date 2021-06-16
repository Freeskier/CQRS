using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Mapper;
using Application.Repos;
using MediatR;
using CommentEntity = Domain.Entities.Comment;

namespace Application.Functions.Comment.Queries
{
    public class GetCommentsForPhotoQueryHandler : IRequestHandler<GetCommentsForPhotoQuery, IEnumerable<CommentResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepo _commentRepo;

        public GetCommentsForPhotoQueryHandler(ICommentRepo commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo ?? throw new ArgumentNullException(nameof(commentRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CommentResponse>> Handle(GetCommentsForPhotoQuery request, CancellationToken cancellationToken)
        {
            var commentsFromRepo = await GetComments(request.Photo.Id);

            var mappedComments = MapCommentsToCommentResponse(commentsFromRepo);

            return mappedComments;
        }

        private async Task<IEnumerable<CommentEntity>> GetComments(Guid photoId)
        {
            var comments = await _commentRepo.GetCommentsForPhotoAsync(photoId);

            return comments;
        }

        private IEnumerable<CommentResponse> MapCommentsToCommentResponse(IEnumerable<CommentEntity> comments)
        {
            IEnumerable<CommentResponse> commentsToReturn;

            _mapper.MapCommentEntityToCommentResponse(comments, out commentsToReturn);

            return commentsToReturn;
        }
    }

        
}
