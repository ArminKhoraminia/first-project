using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string username);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User Login(LoginViewModel login);
        bool ActivationUser(string ActiveCode);
        User GetUserByUserName(string userName);
        string GetEmailByUserName(string userName);
        bool UpdateUsere(User user);
        bool CompareOldPassword(string oldpassword, string username);
        bool UpdatePassword(string password, string username);
        int GetIdByUserName(string user);
        List<Role> GetAllRole();
        int AddUserFromADmin(AddUserForAdminViewModel AddUserForAdmin);
        bool AddRolesForUser(List<int> RoleId , int UserId);
        bool DeleteAllRolesForUser(int UserId);
        User GetUserById(int id);
        List<int> GetUserRolesById(int id);
        bool DeleteUserbyId(int id);

    }
}
