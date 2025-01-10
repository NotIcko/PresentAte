﻿using PresentAte.ViewModels.UserViewModels;

namespace PresentAte.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 5);

        Task<int> GetTotalPagesAsync(int pageSize = 5);

        Task<bool> UserExistsByIdAsync(string userId);

        Task<bool> AssignUserToRoleAsync(string userId, string roleName);

        Task<bool> RemoveUserFromRoleAsync(string userId, string roleName);

        Task<bool> RemoveUserRoleAsync(string userId, string roleName);

        Task<bool> DeleteUserAsync(string userId);
    }
}
