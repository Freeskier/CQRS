using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Mapper;
using Application.Repos;
using Application.Services;
using Domain.Dtos;
using Domain.Entities;
using Domain.Exceptions;

namespace Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepo _messageRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public MessageService(IMessageRepo messageRepo, IMapper mapper, IUserRepo userRepo)
        {
            _messageRepo = messageRepo ?? throw new ArgumentNullException(nameof(messageRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
        }

        public async Task<IEnumerable<MessageResponse>> GetMessagesForConversationAsync(Guid firstUserId, Guid secondUserId, CancellationToken cancellationToken = default)
        {
            var messagesFromRepo = await _messageRepo.GetMessagesForConversationAsync(firstUserId, secondUserId, cancellationToken);

            IEnumerable<MessageResponse> messagesToReturn;

            _mapper.MapMessageEntityToMessageResponse(messagesFromRepo, out messagesToReturn);

            return messagesToReturn;
        }

        public async Task<Message> AddAsync(MessageRequest messageRequest)
        {
            await CheckIfUserExistAsync(messageRequest.ReceiverId);
            
            Message messageToAdd = new();

            _mapper.MapMessageRequestToMessageEntity(messageRequest, ref messageToAdd);

            await _messageRepo.AddAsync(messageToAdd);

            if (!await _messageRepo.SaveAllAsync())
            {
                throw new DatabaseException("Error while updating database.");
            }

            return messageToAdd;
        }

        public async Task<Message> MarkAsReadAsync(Message message)
        {
            message.DateRead = DateTime.UtcNow;

            await _messageRepo.UpdateAsync(message);

            if (!await _messageRepo.SaveAllAsync())
            {
                throw new DatabaseException("Error while updating database.");
            }

            return message;
        }

        public MessageResponse GetSingleMessageAsync(Message message)
        {
            MessageResponse messageToReturn;

            _mapper.MapMessageEntityToMessageResponse(message, out messageToReturn);

            return messageToReturn;
        }

        private async Task CheckIfUserExistAsync(Guid userId)
        {
            var user = await _userRepo.GetSingleUserByIdAsync(userId);

            if (user is null) throw new NotFoundException("User doesn't exist.");
        }
    }
}