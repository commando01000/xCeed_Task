using Common.Layer;
using Data.Layer.Contexts;
using Data.Layer.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Layer.Interfaces;
using Service.Layer.ViewModels.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Layer.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountService(IUnitOfWork<AppDbContext> unitOfWork, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public string? GetCurrentUserId()
        {
            return _userManager.GetUserId(_signInManager.Context.User);
        }

        public async Task<AppUser?> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            return userId == null ? null : await _userManager.FindByIdAsync(userId);
        }

        public async Task<string?> GetCurrentUserDisplayName()
        {
            var user = await GetCurrentUserAsync();
            return user?.DisplayName;
        }

        public async Task<string?> GetCurrentUserEmail()
        {
            var user = await GetCurrentUserAsync();
            return user?.Email;
        }

        public async Task<string?> GetCurrentUserRole()
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }


    }
}
