using DietApp.Data;
using DietApp.MessageSection;
using DietApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IdentityContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly UserManager<DietUser> _userManager;

        public ChatController(IMessageService messageService, IdentityContext context, UserManager<DietUser> userManager,ICurrentUserService currentUser)
        {
            _messageService = messageService;
            _context = context;
            _userManager = userManager;
            _currentUser = currentUser;

        }


        public async Task<IActionResult> Index()
        {
            var users=await _messageService.GetUsers();
            return View(users);
        }

        public async Task<IActionResult> Chat(string selectedUserId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Kullanıcının rollerini al
                var roles = await _userManager.GetRolesAsync(user);

                // Eğer kullanıcının rolü "Hasta" ise, özel layout'u kullanmasını sağla
                if (roles.Contains("Hasta"))
                {
                    ViewBag.Layout = "_HastaLayout";
                }
                else
                {
                    ViewBag.Layout = "_Layout";
                }
            }
            else
            {
                ViewBag.Layout = "_Layout";
            }

            var chatViewModel=await _messageService.GetMessages(selectedUserId);
            return View(chatViewModel);
        }

        public async Task<IActionResult> ChatPatientList()
        {
           

            var currentUserId = _userManager.GetUserId(User);

           

            var patients = await _context.Messages
                .Where(m => m.ReceiverId == currentUserId)
                .Select(m => m.Sender)
                .Distinct()
                .Select(u => new MessageUserListViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    LastMessage = _context.Messages
                        .Where(m => m.SenderId == u.Id && m.ReceiverId == currentUserId ||
                                    m.ReceiverId == u.Id && m.SenderId == currentUserId)
                        .OrderByDescending(m => m.Date)
                        .Select(m => m.Text)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return View(patients);
        }

    }
}
