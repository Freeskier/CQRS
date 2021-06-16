using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Repos;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConversationRepo _conversationRepo;
        private readonly IConnectionRepo _connectionRepo;
        public ConnectionService(IConversationRepo conversationRepo, IConnectionRepo connectionRepo)
        {
            _conversationRepo = conversationRepo ?? throw new ArgumentNullException(nameof(conversationRepo));
            _connectionRepo = connectionRepo ?? throw new ArgumentNullException(nameof(connectionRepo));
        }

        public string CreateConversationName(Guid firstUserId, Guid secondUserId)
        {
            var firstId = firstUserId.ToString();
            var secondId = secondUserId.ToString();

            var compareResult = string.CompareOrdinal(firstId, secondId) < 0;

            return compareResult ? $"{firstId}_{secondId}" : $"{secondId}_{firstId}";
        }
        public async Task DisconnectAsync(Guid userId, CancellationToken cancellationToken)
        {
            var connections = await _connectionRepo.GetConnectionsForUserAsync(userId, cancellationToken);

            foreach (var connection in connections)
            {
                await _connectionRepo.RemoveAsync(connection);
            }

            await SaveAllAsync();
        }

        public async Task ConnectAsync(string conversationName, Guid userId, string connectionId)
        {
            var conversation = await GetConversationAsync(conversationName);

            await AddConnectionAsync(userId, connectionId, conversation);
        }

        public async Task<bool> IsUserConnectedAsync(Guid userId, string conversationName)
        {
           var conversation = await _conversationRepo.GetSingleAsync(conversationName);

           var isConnected = conversation.Connections.Any(x => x.UserId == userId);

           return isConnected;
        }
        private async Task<Conversation> GetConversationAsync(string conversationName)
        {
            var conversation = await _conversationRepo.GetSingleAsync(conversationName);

            if (conversation is null)
            {
                conversation = new Conversation
                {
                    Name = conversationName
                };
            }

            return conversation;
        }

        private async Task AddConnectionAsync(Guid userId, string connectionId, Conversation conversation)
        {
            var connection = new Connection
            {
                ConnectionId = connectionId,
                UserId = userId
            };

            conversation.Connections.Add(connection);

            await SaveAllAsync();
        }

        private async Task SaveAllAsync()
        {
            if (!await _connectionRepo.SaveAllAsync() || !await _conversationRepo.SaveAllAsync())
            {
                throw new DatabaseException("Error while creating conversation.");
            }
        }

        

    }
}