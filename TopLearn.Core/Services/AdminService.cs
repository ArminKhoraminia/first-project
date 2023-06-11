using Azure;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class AdminService : IAdminService
    {
        #region Create Context

        TopLearnContext _dbcontext;
        public AdminService(TopLearnContext context)
        {
            _dbcontext = context;
        }

        #endregion

        #region Get Information
        public GetAllUserForAdminViewModel GetAllUser(int page = 1, string filterEmail = "", string filterUserName = "")
        {
            int take = 3;
            int skip = (page - 1) * take;
            IQueryable<User> list = _dbcontext.Users;

            if (!string.IsNullOrEmpty(filterEmail))
            {
                list = list.Where(u => u.Email.Contains(filterEmail));
            }

            if (!string.IsNullOrEmpty(filterUserName))
            {
                list = list.Where(u => u.UserName.Contains(filterUserName));
            }
            GetAllUserForAdminViewModel arr = new GetAllUserForAdminViewModel();
            arr.CurrentPage = page;
            if ((list.Count() % take) == 0)
                arr.PageCount = list.Count() / take;
            else
                arr.PageCount = (list.Count() / take) + 1;
            arr.Users = list.OrderBy(u => u.UserId).Skip(skip).Take(take).ToList();
            return arr;
        }

        public List<User> GetAllDeleteUser()
        {
            return _dbcontext.Users.IgnoreQueryFilters().OrderBy(u => u.UserId).Where(u => u.IsDelete).ToList();
        }


        public UpdateUserForAdminViewModel ShowUserUpdateAdminPanel(int id)
        {
            User tmpUser = new UserService(_dbcontext).GetUserById(id);
            UpdateUserForAdminViewModel Detail = new UpdateUserForAdminViewModel()
            {
                UserId = tmpUser.UserId,
                Email = tmpUser.Email,
                UserAvatar = tmpUser.UserAvatar,
                UserName = tmpUser.UserName,
                UserRolesInDataBase = new UserService(_dbcontext).GetUserRolesById(id)
            };

            return Detail;

        }

        public DeleteUserForAdminViewModel ShowUserDeleteAdminPanel(int id)
        {
            User tmpUser = new UserService(_dbcontext).GetUserById(id);
            DeleteUserForAdminViewModel Detail = new DeleteUserForAdminViewModel()
            {
                UserId = tmpUser.UserId,
                UserName = tmpUser.UserName,
                Email = tmpUser.Email,
                IsActive = tmpUser.IsActive,
                UserAvatar = tmpUser.UserAvatar,
                RegisterDate = tmpUser.RegisterDate,
                Wallet = new WalletService(_dbcontext).GetWalletPriceUserName(tmpUser.UserName)
            };

            return Detail;

        }

        #endregion

        #region Update 

        public bool UpdateUserFromAdminPanel(UpdateUserForAdminViewModel Detail)
        {
            User tmpUser = new UserService(_dbcontext).GetUserById(Detail.UserId);
            tmpUser.Email = Detail.Email;
            if (!(string.IsNullOrEmpty(Detail.Password)))
            {
                tmpUser.Password = HashPassword.EncodePasswordMd5(Detail.Password);
            }

            if (Detail.NewUserAvatar != null)
            {
                string imagPath = "";
                if (Detail.UserAvatar != "DefultAvatar.jpg")
                {
                    imagPath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/images/UserAvatar", Detail.UserAvatar);
                    if (File.Exists(imagPath))
                    {
                        File.Delete(imagPath);
                    }
                }
                tmpUser.UserAvatar = CodeGenerator.GenerateUniqueCode() + Path.GetExtension(Detail.NewUserAvatar.FileName);
                imagPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images/UserAvatar", tmpUser.UserAvatar);
                using (var stream = new FileStream(imagPath, FileMode.Create))
                {
                    Detail.NewUserAvatar.CopyTo(stream);
                }
            }

            return new UserService(_dbcontext).UpdateUsere(tmpUser);
        }

        #endregion


    }
}
