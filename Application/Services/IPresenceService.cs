using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPresenceService
    {
        Task UserConnected(string userId, string connectionId);
        Task UserDisconnected(string userId, string connectionId);
        Task<IEnumerable<string>> GetOnlineUsers();
        Task<IEnumerable<string>> GetConnectionsForUser(string userId);

    }
}