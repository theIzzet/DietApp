using System.Security.Claims;
using DietApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DietApp.MessageSection
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<DietUser> _userManager;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<DietUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string UserId
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }

        public async Task<DietUser> GetUser()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return null;
            }
            return await _userManager.FindByIdAsync(UserId);
        }
    }
}
