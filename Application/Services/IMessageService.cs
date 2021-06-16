using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Services
{
    public interface IMessageService
    {
        Task<Message> AddAsync(MessageRequest messageRequest);
        Task<IEnumerable<MessageResponse>> GetMessagesForConversationAsync(Guid firstUserId, Guid secondUserId, CancellationToken cancellationToken = default);
        MessageResponse GetSingleMessageAsync(Message message);
        Task<Message> MarkAsReadAsync(Message message);
    }
}