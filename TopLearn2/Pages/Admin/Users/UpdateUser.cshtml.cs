using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using System.Diagnostics.CodeAnalysis;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.Pages.Admin.Users
{
    public class UpdateUserModel : PageModel
    {
        #region Create Context

        private IUserService _userService;
        private IAdminService _AdminService;
        public UpdateUserModel(IUserService userService, IAdminService AdminService)
        {
            _userService = userService;
            _AdminService = AdminService;
        }

        #endregion

        #region Get Information

        public IActionResult OnGet(int id)
        {
            UpdateUserForAdmin = _AdminService.ShowUserUpdateAdminPanel(id);
            ViewData["AllRoles"] = _userService.GetAllRole();
            return Page();
        }

        #endregion

        #region Update 

        [BindProperty]
        public UpdateUserForAdminViewModel UpdateUserForAdmin { get; set; }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            bool isSucces;
            UpdateUserForAdmin.UserRolesInDataBase = SelectedRoles;
            ViewData["AllRoles"] = _userService.GetAllRole();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            int tmpUserId = UpdateUserForAdmin.UserId;
            isSucces = _AdminService.UpdateUserFromAdminPanel(UpdateUserForAdmin);

            if (isSucces)
                isSucces = _userService.DeleteAllRolesForUser(tmpUserId);
            else
                return Page();

            if (SelectedRoles != null)
            {
                if (isSucces)
                    isSucces = _userService.AddRolesForUser(SelectedRoles, tmpUserId);
                else
                    return Page();
            }

            return Redirect("/Admin/Users");
        }

        #endregion
    }
}
