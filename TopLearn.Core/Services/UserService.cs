using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class UserService : IUserService
    {
        #region Create Context

        TopLearnContext _context;
        public UserService(TopLearnContext context)
        {
            _context = context;
        }

        #endregion

        #region Get Information

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistUserName(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }
        public string GetEmailByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName).Email;
        }
        public int GetIdByUserName(string user)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == user).UserId;
        }

        public bool CompareOldPassword(string oldpassword, string username)
        {
            string hashOldPassword = HashPassword.EncodePasswordMd5(oldpassword);
            return _context.Users.Any(u => u.UserName == username && u.Password == hashOldPassword);
        }
        public User Login(LoginViewModel login)
        {
            string tmpemail = FixedText.TrimeAndLowerText(login.Email);
            string tmppass = HashPassword.EncodePasswordMd5(login.Password);
            return _context.Users.SingleOrDefault(u => u.Email == tmpemail && u.Password == tmppass);
        }

        public List<Role> GetAllRole()
        {
            return _context.Roles.ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public List<int> GetUserRolesById(int id)
        {
            return _context.UserRoles.Where(r => r.UserId == id).Select(r => r.RoleId).ToList();
        }

        #endregion

        #region Add User

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public int AddUserFromADmin(AddUserForAdminViewModel AddUserForAdmin)
        {
            User tmpUser = new User();
            tmpUser.UserName = AddUserForAdmin.UserName;
            tmpUser.Password = HashPassword.EncodePasswordMd5(AddUserForAdmin.Password);
            tmpUser.Email = AddUserForAdmin.Email;
            tmpUser.ActiveCode = CodeGenerator.GenerateUniqueCode();
            tmpUser.IsActive = true;
            tmpUser.RegisterDate = DateTime.Now;

            if (AddUserForAdmin.UserAvatar != null)
            {
                string imagPath = "";
                tmpUser.UserAvatar = CodeGenerator.GenerateUniqueCode() + Path.GetExtension(AddUserForAdmin.UserAvatar.FileName);
                imagPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images/UserAvatar", tmpUser.UserAvatar);
                using (var stream = new FileStream(imagPath, FileMode.Create))
                {
                    AddUserForAdmin.UserAvatar.CopyTo(stream);
                }
            }

            return AddUser(tmpUser);
        }

        #endregion

        #region Add Role To User

        public bool AddRolesForUser(List<int> RoleId, int UserId)
        {
            foreach (int item in RoleId)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = item,
                    UserId = UserId
                });
            }
            _context.SaveChanges();
            return true;
        }
        public bool DeleteAllRolesForUser(int UserId)
        {
            _context.UserRoles.Where(r => r.UserId == UserId).ToList().ForEach(r=> _context.Remove(r));
            _context.SaveChanges();
            return true;
        }

        #endregion

        #region Update User

        public bool ActivationUser(string ActiveCode)
        {
            var tmpuser = _context.Users.SingleOrDefault(u => u.ActiveCode == ActiveCode);
            if (tmpuser == null || tmpuser.IsActive) return false;

            tmpuser.IsActive = true;
            tmpuser.ActiveCode = CodeGenerator.GenerateUniqueCode();
            _context.SaveChanges();
            return true;
        }

        public bool UpdateUsere(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return true;
        }

        public bool UpdatePassword(string password, string username)
        {
            var tmpUser = GetUserByUserName(username);
            string hashPassword = HashPassword.EncodePasswordMd5(password);
            tmpUser.Password = hashPassword;
            UpdateUsere(tmpUser);
            return true;
        }

        #endregion

        #region Delete User

        public bool DeleteUserbyId(int id)
        {
            User tmpUser = GetUserById(id);
            tmpUser.IsDelete = true;
            _context.SaveChanges();
            return true;
        }

        #endregion


    }
}
