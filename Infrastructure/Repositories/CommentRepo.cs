using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repos
{
    
    public class CommentRepo : GenericRepo<Comment>, ICommentRepo
    {
        public CommentRepo(ApplicationDataContext dataContext) : base(dataContext) { }

        public async Task<Comment> GetSingleAsync(Guid id)
        {
            var comment = await _dataContext.Comments.FindAsync(id);

            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsForPhotoAsync(Guid photoId)
        {
            var comments = await _dataContext
                .Comments
                .Where(c => c.PhotoId == photoId)
                .ToListAsync();

            return comments;
        }
    }
}