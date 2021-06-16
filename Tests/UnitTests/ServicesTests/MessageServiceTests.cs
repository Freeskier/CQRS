using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Mapper;
using Application.Repos;
using Domain.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.DataAccess;
using Infrastructure.Mappers;
using Infrastructure.Services;
using NSubstitute;
using Xunit;

namespace UnitTests.ServicesTests
{
    public class MessageServiceTests
    {
        private readonly IMessageRepo _messageRepo = Substitute.For<IMessageRepo>();
        private readonly IUserRepo _userRepo = Substitute.For<IUserRepo>();
        private readonly IMapper _mapper;
        private readonly MessageService _messageService;

        public MessageServiceTests()
        {
            _mapper = new Mapper();
            _messageService = new MessageService(_messageRepo, _mapper, _userRepo);
        }

        [Fact]
        public async Task GetMessagesForConversationAsync_ShouldReturnEmptyIEnumerable()
        {
            // Arrange
            var firstUserId = Guid.NewGuid();
            var secondUserId = Guid.NewGuid();
            var emptyList = new List<Message>();
            var emptyListResponse = new List<MessageResponse>();

            _messageRepo.GetMessagesForConversationAsync(firstUserId, secondUserId).Returns(emptyList);

            // Act
            var messages = await _messageService.GetMessagesForConversationAsync(firstUserId, secondUserId);

            // Assert
            Assert.Equal(emptyListResponse, messages);
        }

        [Fact]
        public async Task GetMessagesForConversationAsync_ShouldReturnTwoValues()
        {
            // Arrange
            var firstUserId = Guid.NewGuid();
            var secondUserId = Guid.NewGuid();
            var testMessage = new Message()
            {
                Id = Guid.NewGuid(),
                SenderId = firstUserId,
                ReceiverId = secondUserId,
                Receiver = new(),
                Sender = new(),
                Content = "Test content"
            };

            var repoList = new List<Message>() { 
                testMessage,
                testMessage
            };

            _messageRepo.GetMessagesForConversationAsync(firstUserId, secondUserId).Returns(repoList);

            // Act
            var messages = await _messageService.GetMessagesForConversationAsync(firstUserId, secondUserId);

            // Assert
            Assert.Equal(repoList.Count, messages.Count());
        }

        [Fact]
        public async Task GetMessagesForConversationAsync_ShuldEqualContentAndIds()
        {
            // Arrange
            var firstUserId = Guid.NewGuid();
            var secondUserId = Guid.NewGuid();
            var testMessage = new Message()
            {
                Id = Guid.NewGuid(),
                SenderId = firstUserId,
                ReceiverId = secondUserId,
                Receiver = new(),
                Sender = new(),
                Content = "Test content"
            };

            var repoList = new List<Message>() { 
                testMessage
            };

            _messageRepo.GetMessagesForConversationAsync(firstUserId, secondUserId).Returns(repoList);

            // Act
            var messages = await _messageService.GetMessagesForConversationAsync(firstUserId, secondUserId);

            // Assert
            Assert.Equal(repoList.ElementAt(0).Content, messages.ElementAt(0).Content);
            Assert.Equal(repoList.ElementAt(0).SenderId, messages.ElementAt(0).SenderId);
            Assert.Equal(repoList.ElementAt(0).ReceiverId, messages.ElementAt(0).ReceiverId);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowDatabaseException()
        {
            // Arrange
            var senderId = Guid.NewGuid();
            var receiverId = Guid.NewGuid();

            var message = new MessageRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId
            };

            _userRepo.GetSingleUserByIdAsync(receiverId).Returns(new AppUser());
            _messageRepo.SaveAllAsync().Returns(false);

            // Act
            var task = _messageService.AddAsync(message);

            // Assert
            await Assert.ThrowsAsync<DatabaseException>(async () => {
                await task;
            });
        }

        [Fact]
        public async Task AddAsync_ShouldThrowNotFoundException()
        {
            // Arrange
            var senderId = Guid.NewGuid();
            var receiverId = Guid.NewGuid();

            var message = new MessageRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId
            };

            _userRepo.GetSingleUserByIdAsync(receiverId).Returns((AppUser)null);
            _messageRepo.SaveAllAsync().Returns(true);

            // Act
            var task = _messageService.AddAsync(message);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => {
                await task;
            });
        }
        
    }
}