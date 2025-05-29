using Common.Layer;
using Data.Layer.Entities.Identity;
using Service.Layer.ViewModels.Authentication;

namespace Service.Layer.Services.Account
{
    public interface IAccountService
    {
        public string? GetCurrentUserId();
        public Task<AppUser?> GetCurrentUserAsync();
        public Task<string?> GetCurrentUserDisplayName();
        public Task<string?> GetCurrentUserEmail();
        public Task<string?> GetCurrentUserRole();
    }
}
