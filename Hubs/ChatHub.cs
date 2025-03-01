using DietApp.Data;
using DietApp.Entities;
using DietApp.MessageSection;
using Microsoft.AspNetCore.SignalR;

namespace DietApp.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IdentityContext _context;
        private readonly ICurrentUserService currentUserService;

        public ChatHub(IdentityContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            this.currentUserService = currentUserService;
        }



        public async Task SendMessage(string receiverId, string message)
        {
            var NowDate = DateTime.Now;
            string senderId = currentUserService.UserId;

            if (string.IsNullOrEmpty(senderId))
            {
                throw new Exception("Giriş yapmış kullanıcı bulunamadı.");
            }

            var messageToAdd = new Message
            {
                Text = message,
                Date = NowDate,
                SenderId = senderId,
                ReceiverId = receiverId,
            };

            await _context.AddAsync(messageToAdd);
            await _context.SaveChangesAsync();

            List<string> users = new List<string>()
        {
            receiverId, senderId
        };

            await Clients.Users(users).SendAsync("ReceiveMessage", message, NowDate.ToShortDateString(), NowDate.ToShortTimeString(), senderId);
        }
    }
}
