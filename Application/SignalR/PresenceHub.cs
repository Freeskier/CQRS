using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly IPresenceService _presenceService;
        public PresenceHub(IPresenceService presenceService)
        {
            _presenceService = presenceService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.GetUserId();
            var connectionId = Context.ConnectionId;

            await _presenceService.UserConnected(userId.ToString(), connectionId);

            await Clients.Others.SendAsync("UserIsOnline", userId);

            await GetOnlineUsers();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.GetUserId();
            var connectionId = Context.ConnectionId;

            await _presenceService.UserDisconnected(userId.ToString(), connectionId);

            await Clients.Others.SendAsync("UserIsOffline", userId);

            await GetOnlineUsers();

            await base.OnDisconnectedAsync(exception);
        }

        private async Task GetOnlineUsers()
        {
            var currentUsers = await _presenceService.GetOnlineUsers();

            await Clients.All.SendAsync("AllOnlineUsers", currentUsers);
        }
    }
}