using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn2.Web.Pages.Admin.Users
{
    public class DeleteUserListModel : PageModel
    {
        #region Create Context

        private IAdminService _adminService;
        public DeleteUserListModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        public List<User> Users { get; set; }
        public void OnGet()
        {
            Users = _adminService.GetAllDeleteUser();
        }
    }
}
