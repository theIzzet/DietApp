using DietApp.Data;
using DietApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DietApp.MessageSection
{
    public class MessageService:IMessageService
    {
        private readonly IdentityContext _context;
        private readonly ICurrentUserService _currentUserService;
        public MessageService(IdentityContext context,ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

      

        public async Task<ChatViewModel> GetMessages(string selectedUserId)
        {
            var currentUserId = _currentUserService.UserId;
            var selectedUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == selectedUserId);
            var selectedUserName = selectedUser?.UserName ?? "Unknown";

            var chatViewModel = new ChatViewModel()
            {
                CurrentUserId = currentUserId,
                ReceiverId = selectedUserId,
                ReceiverUserName = selectedUserName
            };

            var messages = await _context.Messages
                .Where(m => (m.SenderId == currentUserId && m.ReceiverId == selectedUserId) ||
                            (m.SenderId == selectedUserId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.Date) // Mesajları tarihe göre sırala
                .Select(m => new UserMessageListViewModel()
                {
                    Id = m.Id,
                    Message = m.Text,
                    Date = m.Date.ToShortDateString(),
                    Time = m.Date.ToShortTimeString(),
                    IsCurrentUserSentMessage = m.SenderId == currentUserId
                })
                .ToListAsync();

            chatViewModel.Messages = messages;
            return chatViewModel;
        }

        public async Task<IEnumerable<MessageUserListViewModel>> GetUsers()
        {
            var currentUserId= (_currentUserService.UserId);
            var users=await _context.Users.Where(i=>i.Id!=currentUserId).Select(i=> new MessageUserListViewModel()
            {
                Id=i.Id,
                UserName=i.UserName,

                LastMessage = _context.Messages.Where(m => (m.SenderId == currentUserId || m.SenderId == i.Id) && (m.ReceiverId == currentUserId || m.ReceiverId == i.Id)).OrderByDescending(m => m.Id).Select(m => m.Text).FirstOrDefault()

            }).ToListAsync();

            return users;
        }
    }
}
