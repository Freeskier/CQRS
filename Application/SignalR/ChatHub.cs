using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IConnectionService _connectionService;
        private readonly IPresenceService _presenceService;
        private readonly IHubContext<PresenceHub> _presenceHub;
        private const string NewMessageNotification = "NewMessageReceivedWithNotification";
        private const string UpdateLastMessage = "UpdateLastMessage";
        private const string NewMessageWithOpenedConversation = " NewMessageReceivedWithOpenedConversation";
        

        public ChatHub(IMessageService messageService, IConnectionService connectionService, IPresenceService presenceService, IHubContext<PresenceHub> presenceHub)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _connectionService = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
            _presenceService = presenceService ?? throw new ArgumentNullException(nameof(presenceService));
            _presenceHub = presenceHub ?? throw new ArgumentNullException(nameof(PresenceHub));
        }

        public override async Task OnConnectedAsync()
        {
            Guid userId = await GetCallerAndDeleteAllConnectionsAsync();
            Guid otherUserId;
            string conversationName;

            CreateConversationName(userId, out otherUserId, out conversationName);

            await AddUserToConnectionGroupAsync(userId, conversationName);

            await SendAllMessagesAsync(userId, otherUserId, conversationName);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await GetCallerAndDeleteAllConnectionsAsync();
            await base.OnDisconnectedAsync(exception);
        }

        private async Task<Guid> GetCallerAndDeleteAllConnectionsAsync()
        {
            var userId = Context.User.GetUserId();

            await _connectionService.DisconnectAsync(userId);

            return userId;
        }

        private async Task SendMessageAsync(MessageRequest messageRequest)
        {
            var messageTask = _messageService.AddAsync(messageRequest);

            var senderConnections = await _presenceService.GetConnectionsForUser(messageRequest.SenderId.ToString());
            var receiverConnections = await _presenceService.GetConnectionsForUser(messageRequest.ReceiverId.ToString());

            var conversationName = _connectionService.CreateConversationName(messageRequest.SenderId, messageRequest.ReceiverId);
            var isReceiverConnected = await _connectionService.IsUserConnectedAsync(messageRequest.ReceiverId, conversationName);

            var message = await messageTask;
            var messageToReturn = _messageService.GetSingleMessageAsync(message);

            if (isReceiverConnected)
            {
                message = await SendNotificationWhenUserIsInConversationAsync(conversationName, message, messageToReturn);
            }
            else
            {
                await SendNotificationWhenUserIsNotInConversationAsync(receiverConnections, messageToReturn);
            }

            await SendUpdateLastMessageNotificationAsync(senderConnections, receiverConnections, messageToReturn);

        }

        private async Task SendNotificationWhenUserIsNotInConversationAsync(IEnumerable<string> receiverConnections, MessageResponse messageToReturn)
        {
            if (receiverConnections is not null)
            {
                await _presenceHub.Clients.Clients(receiverConnections).SendAsync(NewMessageNotification,
                new
                {
                    username = messageToReturn.SenderUserName
                });   
            }
        }

        private async Task<Domain.Entities.Message> SendNotificationWhenUserIsInConversationAsync(string conversationName, Domain.Entities.Message message, MessageResponse messageToReturn)
        {
            message = await _messageService.MarkAsReadAsync(message);

            await Clients.Group(conversationName)
                .SendAsync(NewMessageWithOpenedConversation, messageToReturn);

            return message;
        }

        private async Task SendUpdateLastMessageNotificationAsync(IEnumerable<string> senderConnections, IEnumerable<string> receiverConnections, MessageResponse messageToReturn)
        {
            // Sends notification with message sent, so last message in conversation list can be updated without getting all conversation

            if (senderConnections is not null)
            {
                await Clients.Clients(senderConnections).SendAsync(UpdateLastMessage, messageToReturn);
            }

            if (receiverConnections is not null)
            {
                await Clients.Clients(receiverConnections).SendAsync(UpdateLastMessage, messageToReturn);
            }
        }

        private async Task SendAllMessagesAsync(Guid userId, Guid otherUserId, string conversationName)
        {
            var messageThread = _messageService.GetMessagesForConversationAsync(userId, otherUserId);

            await Clients.Group(conversationName).SendAsync("GetMessageThread", messageThread);
        }

        private void CreateConversationName(Guid userId, out Guid otherUserId, out string conversationName)
        {
            var httpContext = Context.GetHttpContext();
            var otherUserFromQuery = httpContext.Request.Query["user"].ToString();
            otherUserId = Guid.Parse(otherUserFromQuery);
            conversationName = _connectionService.CreateConversationName(userId, otherUserId);
        }

        private async Task AddUserToConnectionGroupAsync(Guid userId, string conversationName)
        {
            var connectionId = Context.ConnectionId;

            await Groups.AddToGroupAsync(connectionId, conversationName);
            await _connectionService.ConnectAsync(conversationName, userId, connectionId);
        }

        
    }
}