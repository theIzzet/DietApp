using DietApp.Data;

namespace DietApp.MessageSection
{
    public interface ICurrentUserService
    {
        string UserId { get;  }
        Task<DietUser> GetUser();
    }
}
