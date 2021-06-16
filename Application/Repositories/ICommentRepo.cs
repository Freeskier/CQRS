using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repos
{
    public interface ICommentRepo : IGenericRepo<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(Guid photoId);
        Task<Comment> GetSingleAsync(Guid id);
    }
}