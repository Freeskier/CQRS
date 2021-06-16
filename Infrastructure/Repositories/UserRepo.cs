using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Repos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repos
{
    public class UserRepo : GenericRepo<AppUser>, IUserRepo
    {
        public UserRepo(ApplicationDataContext databaseContext) : base(databaseContext) { }
   
        public async Task<IEnumerable<AppUser>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            var users = await _dataContext.Users.ToListAsync(cancellationToken);

            return users;
        }

        public async Task<AppUser> GetSingleUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

            return user;
        }

        public async Task<AppUser> GetSingleUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return user;
        }

        public async Task<AppUser> GetSingleUserByUsernameAsync(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);

            return user;
        }

        public async Task<IEnumerable<AppUser>> GetUsersWithKeyWord(string keyWord, CancellationToken cancellationToken = default)
        {
            keyWord = keyWord.ToLower();

            var users = await _dataContext.Users
                .Where(u => u.UserName.Contains(keyWord)
                || u.FirstName.Contains(keyWord)
                || u.LastName.Contains(keyWord))
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}