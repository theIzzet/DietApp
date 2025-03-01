using DietApp.Models;

namespace DietApp.MessageSection
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageUserListViewModel>> GetUsers();
        Task<ChatViewModel> GetMessages(string selectedUserId);
    }
}
