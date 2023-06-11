using System.Reflection.Metadata.Ecma335;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;

namespace TopLearn.Core.Services
{
    public class UserPanelService : IUserPanelService
    {
        #region Create Context
        TopLearnContext _dbcontext;
        public UserPanelService(TopLearnContext context)
        {
            _dbcontext = context;
        }
        #endregion

        #region Get Information 
        public DetailUsePanelViewModel GetDetailUserPanell(string username)
        {
            UserService tmp = new UserService(_dbcontext);
            var user = tmp.GetUserByUserName(username);
            DetailUsePanelViewModel detailUsePanel = new DetailUsePanelViewModel();
            detailUsePanel.UserId = user.UserId;
            detailUsePanel.UserName = user.UserName;
            detailUsePanel.UserAvatar = user.UserAvatar;
            detailUsePanel.Email = user.Email;
            detailUsePanel.RegisterDate = user.RegisterDate;
            detailUsePanel.PriceOfWallet = new WalletService(_dbcontext).GetWalletPriceUserName(username);
            return detailUsePanel;
        }

        public SideBarUserPanelViewModel GetSideBarUserPanel(string username)
        {
            UserService tmp = new UserService(_dbcontext);
            var user = tmp.GetUserByUserName(username);
            SideBarUserPanelViewModel SideBarUsePanel = new SideBarUserPanelViewModel();
            SideBarUsePanel.UserName = user.UserName;
            SideBarUsePanel.UserAvatar = user.UserAvatar;
            SideBarUsePanel.RegisterDate = user.RegisterDate;
            return SideBarUsePanel;
        }

        public EditeUserPanelViewModel GetEditeUserPanel(string username)
        {
            UserService tmp = new UserService(_dbcontext);
            var user = tmp.GetUserByUserName(username);
            EditeUserPanelViewModel EditeUsePanel = new EditeUserPanelViewModel();
            EditeUsePanel.UserName = user.UserName;
            EditeUsePanel.OldUserAvatar = user.UserAvatar;
            EditeUsePanel.Email = user.Email;
            return EditeUsePanel;
        }

        #endregion

        #region Update User

        public bool EditeUserPanel(string oldusername, EditeUserPanelViewModel UserPanel)
        {
            if (UserPanel.UserAvatar != null)
            {
                string imagPath = "";
                if (UserPanel.OldUserAvatar != "DefultAvatar.jpg")
                {
                    imagPath = Path.Combine(Directory.GetCurrentDirectory(), 
                        "wwwroot/images/UserAvatar", UserPanel.OldUserAvatar);
                    if (File.Exists(imagPath)) 
                    {
                        File.Delete(imagPath);
                    }
                }

                UserPanel.OldUserAvatar = CodeGenerator.GenerateUniqueCode() + Path.GetExtension(UserPanel.UserAvatar.FileName);
                imagPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images/UserAvatar", UserPanel.OldUserAvatar);
                using (var stream = new FileStream(imagPath, FileMode.Create))
                {
                    UserPanel.UserAvatar.CopyTo(stream);
                }
            }

            UserService user = new UserService(_dbcontext);
            var tmpuser = user.GetUserByUserName(oldusername);
            tmpuser.UserName = UserPanel.UserName;
            tmpuser.Email = UserPanel.Email;
            tmpuser.UserAvatar = UserPanel.OldUserAvatar;

            user.UpdateUsere(tmpuser);

            return true;
        }

        #endregion 
    }
}
