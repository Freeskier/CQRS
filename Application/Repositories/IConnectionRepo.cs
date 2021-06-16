using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repos
{
    public interface IConnectionRepo : IGenericRepo<Connection>
    {
        Task<IEnumerable<Connection>> GetConnectionsForUserAsync(Guid userId, CancellationToken cancellationToken);
        Task<Connection> GetSingleAsync(string connectionId);
    }
}