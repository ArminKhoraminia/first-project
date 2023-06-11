using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.Pages.Admin.Users
{
    public class DeleteUserModel : PageModel
    {
        #region Create Context

        private IAdminService _adminService;
        private IUserService _userService;
        public DeleteUserModel(IAdminService adminService , IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        #endregion

        #region Get Information

        public void OnGet(int id)
        {
            DeleteUserForAdmin = _adminService.ShowUserDeleteAdminPanel(id);
        }

        #endregion

        #region Delete User

        [BindProperty]
        public DeleteUserForAdminViewModel DeleteUserForAdmin { get; set; }
        public IActionResult OnPost()
        {
            bool isSucces = _userService.DeleteUserbyId(DeleteUserForAdmin.UserId);
            if (isSucces) 
            { 
                return Redirect("/Admin/Users");
            }
            else
            {
                return Page();  
            }

        }

        #endregion
    }
}
