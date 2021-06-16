using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Repos;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repos
{
    public class ConnectionRepo : GenericRepo<Connection>, IConnectionRepo
    {
        public ConnectionRepo(ApplicationDataContext dataContext) : base(dataContext) { }

        public async Task<Connection> GetSingleAsync(string connectionId)
        {
            var connection = await _dataContext.Connections.FindAsync(connectionId);

            return connection;
        }

        public async Task<IEnumerable<Connection>> GetConnectionsForUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var connections = await _dataContext.Connections.Where(c => c.UserId == userId)
                .ToListAsync(cancellationToken);

            return connections;
        }
    }
}