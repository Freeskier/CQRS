using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services
{
     public interface IConnectionService
    {
        Task ConnectAsync(string conversationName, Guid userId, string connectionId);
        string CreateConversationName(Guid firstUserId, Guid secondUserId);
        Task DisconnectAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> IsUserConnectedAsync(Guid userId, string conversationName);
    }
}