using System.Threading.Tasks;
using Application.Repos;
using Domain.Entities;

namespace Infrastructure.DataAccess.Repos
{
    public class ConversationRepo : GenericRepo<Conversation>, IConversationRepo
    {
        public ConversationRepo(ApplicationDataContext dataContext) : base(dataContext) { }

        public async Task<Conversation> GetSingleAsync(string name)
        {
            var conversation = await _dataContext.Conversations.FindAsync(name);

            return conversation;
        }
    }
}