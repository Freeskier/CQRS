using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Repos;
using Application.Services;
using Application.SignalR;
using Domain.Dtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NSubstitute;
using Xunit;

namespace UnitTests.HubsTests
{
    public class ChatHubTests
    {
        private readonly Mock<IHubCallerClients> _clients = new Mock<IHubCallerClients>();
        private readonly Mock<IClientProxy> _clientProxy = new Mock<IClientProxy>();
        private readonly IMessageService _messageService = Substitute.For<IMessageService>();
        private readonly IConnectionService _connectionService;
        private readonly IPresenceService _presenceService = Substitute.For<IPresenceService>();
        private readonly IHubContext<PresenceHub> _presenceHub = Substitute.For<IHubContext<PresenceHub>>();
        private readonly ChatHub _chatHub;

        public ChatHubTests()
        {
            _connectionService = new ConnectionService(Substitute.For<IConversationRepo>(), Substitute.For<IConnectionRepo>());
            _chatHub = new ChatHub(_messageService, _connectionService, _presenceService, _presenceHub);
        } 

        [Fact]
        public async Task OnConnectedAsync_ShouldReturn3Messages()
        {
            // Arrange
            var firstUserId = Guid.NewGuid();
            var secondUserId = Guid.NewGuid();
            var groupName = _connectionService.CreateConversationName(firstUserId, secondUserId);

            var message = new MessageResponse 
            {
                Id = Guid.NewGuid(),
                SenderId = firstUserId,
                ReceiverId = secondUserId,
                Content = "Test Message",
                DateRead = DateTime.UtcNow,
                DateSent = DateTime.UtcNow.AddYears(-2)
            };

            _clients.Setup(x => x.Group(groupName)).Returns(_clientProxy.Object);
            _chatHub.Clients = _clients.Object;
            _chatHub.Context = Substitute.For<HubCallerContext>();
            //_chatHub.Context.User.FindFirst(ClaimTypes.NameIdentifier).Returns(new Claim());

            _chatHub.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value.Returns(firstUserId.ToString());
            //_chatHub.Context.GetHttpContext().Request.Query["user"].ToString().Returns(secondUserId.ToString());
            _messageService.GetMessagesForConversationAsync(firstUserId, secondUserId).Returns(new MessageResponse[2] {message, message});

            // Act
            await _chatHub.OnConnectedAsync();

            // Asert
            _clients.Verify(clients => clients.Group(groupName), Times.Once);
        }
    }
}