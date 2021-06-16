using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Repos;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repos
{
    public class MessageRepo : GenericRepo<Message>, IMessageRepo
    {
        public MessageRepo(ApplicationDataContext dataContext) : base(dataContext) { }

        public async Task<IEnumerable<Message>> GetMessagesForConversationAsync(Guid firstUserId, Guid secondUserId, CancellationToken cancellationToken = default)
        {
            var messages = await _dataContext
                .Messages
                .Where(message =>
                (message.ReceiverId == firstUserId && message.SenderId == secondUserId) ||
                (message.ReceiverId == secondUserId && message.SenderId == firstUserId))
                .ToListAsync(cancellationToken);

            return messages;
        }

    }
}