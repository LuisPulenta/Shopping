using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shooping.Api.Helpers;
using Shooping.Data;
using Shopping.Data.Entities;

namespace Shopping.Api.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users
                .Include(u=>u.City)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        //public async Task<SignInResult> LoginAsync(LoginViewModel model)
        //{
        //    return await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        //}

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id.ToString());
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            User currentUser = await GetUserAsync(user.Email);
            currentUser.LastName = user.LastName;
            currentUser.FirstName = user.FirstName;
            currentUser.Document = user.Document;
            currentUser.Address = user.Address;
            currentUser.ImageId = user.ImageId;
            currentUser.PhoneNumber = user.PhoneNumber;
            return await _userManager.UpdateAsync(currentUser);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        //public async Task<User> AddUserAsync(AddUserViewModel model, string imageId, UserType userType)
        //{
        //    User user = new User
        //    {
        //        Address = model.Address,
        //        Document = model.Document,
        //        Email = model.Username,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        ImageId = imageId,
        //        PhoneNumber = model.PhoneNumber,
        //        DocumentType = await _context.DocumentTypes.FindAsync(model.DocumentTypeId),
        //        UserName = model.Username,
        //        UserType = userType
        //    };

        //    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        //    if (result != IdentityResult.Success)
        //    {
        //        return null;
        //    }

        //    User newUser = await GetUserAsync(model.Username);
        //    await AddUserToRoleAsync(newUser, user.UserType.ToString());
        //    return newUser;
        //}

        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<SignInResult> ValidatePasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

    }
}