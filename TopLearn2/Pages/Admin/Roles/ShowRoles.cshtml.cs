using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn2.Web.Pages.Admin.Roles
{
    [PermissionChecker(6)]
    public class ShowRolesModel : PageModel
    {
        #region Create Context

        private IUserService _userService;
        public ShowRolesModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Get Information

        public List<Role> Roles { get; set; }
        public void OnGet()
        {
            Roles = _userService.GetAllRole();
        }

        #endregion

    }
}
