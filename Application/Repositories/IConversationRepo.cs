using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repos
{
    public interface IConversationRepo : IGenericRepo<Conversation>
    {
        Task<Conversation> GetSingleAsync(string name);
    }
}