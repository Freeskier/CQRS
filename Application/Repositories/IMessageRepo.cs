using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repos
{
    public interface IMessageRepo : IGenericRepo<Message>
    {
        Task<IEnumerable<Message>> GetMessagesForConversationAsync(Guid firstUserId, Guid secondUserId, CancellationToken cancellationToken = default);
    }
}