namespace DietApp.Models
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Messages = new List<UserMessageListViewModel>();
        }


        public string CurrentUserId { get; set; }
        public string ReceiverId { get; set; }

        public string ReceiverUserName { get; set; }

        public IEnumerable<UserMessageListViewModel> Messages { get; set; }
    }
}

