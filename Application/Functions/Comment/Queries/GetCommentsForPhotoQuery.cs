using System.Collections.Generic;
using Domain.Dtos;
using MediatR;

namespace Application.Functions.Comment.Queries
{
    public class GetCommentsForPhotoQuery : IRequest<IEnumerable<CommentResponse>>
    {
        public GuidRequest Photo { get; set; }
    }
}