using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;

namespace Infrastructure.Services
{
    public class PresenceService : IPresenceService
    {
        private readonly Dictionary<string, List<string>> onlineUsers = new();
        public Task<IEnumerable<string>> GetConnectionsForUser(string userId)
        {
            var result = LockGetters(() => 
            {
                return onlineUsers.GetValueOrDefault(userId);
            });

            return Task.FromResult(result);
        }

        public Task<IEnumerable<string>> GetOnlineUsers()
        {
            var result = LockGetters(() => 
            {
                return onlineUsers.OrderBy(k => k.Key)
                    .Select(k => k.Key)
                    .ToArray();
            });

            return Task.FromResult(result);
        }

        public Task UserConnected(string userId, string connectionId)
        {
            LockAction(() =>
            {
                ConnectUser(userId, connectionId);
            }); 

            return Task.CompletedTask;
        }

        private void ConnectUser(string userId, string connectionId)
        {
            if (onlineUsers.ContainsKey(userId))
            {
                onlineUsers[userId].Add(connectionId);
            }
            else
            {
                onlineUsers.Add(userId, new List<string> { connectionId });
            }
        }

        public Task UserDisconnected(string userId, string connectionId)
        {
            LockAction(() =>
            {
                DisconnectUser(userId, connectionId);
            });

            return Task.CompletedTask;
        }

        private void DisconnectUser(string userId, string connectionId)
        {
            if (!onlineUsers.ContainsKey(userId))
            {
                return;
            }

            onlineUsers[userId].Remove(connectionId);

            if (onlineUsers[userId].Count == 0)
            {
                onlineUsers.Remove(userId);
            }
        }

        private void LockAction(Action action)
        {
            lock(onlineUsers)
            {
                action();
            }
        }

        private IEnumerable<string> LockGetters(Func<IEnumerable<string>> func)
        {
            IEnumerable<string> result;

            lock(onlineUsers)
            {
                result = func();
            }

            return result;
        }
    }
}