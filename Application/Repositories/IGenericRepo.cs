using System.Threading;
using System.Threading.Tasks;

namespace Application.Repos
{
    public interface IGenericRepo<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> SaveAllAsync();
    }
}