using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn2.Web.Pages.Admin.Roles
{
    [PermissionChecker(7)]
    public class CreateRoleModel : PageModel
    {
        #region Create Context

        private IPermissionService _permissionService;
        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        #endregion

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            bool isSucces;
            if (!ModelState.IsValid)
                return Page();

            int tmpRoleId = _permissionService.AddRole(Role);

            isSucces = _permissionService.AddRolePermission(tmpRoleId, SelectedPermission);

            if (isSucces)
                return Redirect("/Admin/Roles/ShowRoles");
            else
                return Page();
        }
    }
}
