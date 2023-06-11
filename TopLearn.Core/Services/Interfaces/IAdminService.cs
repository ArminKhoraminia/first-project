using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IAdminService
    {
        GetAllUserForAdminViewModel GetAllUser(int page = 1, string filterEmail = "", string filterUserName = "");
        List<User> GetAllDeleteUser();
        UpdateUserForAdminViewModel ShowUserUpdateAdminPanel(int id);
        bool UpdateUserFromAdminPanel(UpdateUserForAdminViewModel Detail);
        DeleteUserForAdminViewModel ShowUserDeleteAdminPanel(int id);
    }
}
