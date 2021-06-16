using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Repos;

namespace Infrastructure.DataAccess.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly ApplicationDataContext _dataContext;

        public GenericRepo(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dataContext.AddAsync<T>(entity, cancellationToken);
        }

        public Task RemoveAsync(T entity)
        {
            _dataContext.Remove<T>(entity);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            _dataContext.Update(entity);

            return Task.CompletedTask;
        }

        public async Task<bool> SaveAllAsync()
        {
            var result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }
    }
}