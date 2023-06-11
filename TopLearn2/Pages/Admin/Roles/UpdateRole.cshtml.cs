using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn2.Web.Pages.Admin.Roles
{
    [PermissionChecker(8)]
    public class UpdateRoleModel : PageModel
    {
        #region Create Context

        private IPermissionService _permissionService;
        public UpdateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        #endregion

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleById(id);
            ViewData["SelectedPermissions"] = _permissionService.GetPermissionsByRoleId(id);
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
        }

        public IActionResult OnPost(List<int> SelectedPermission)
        {
            bool isSucces;
            if (!ModelState.IsValid)
            {
                ViewData["SelectedPermissions"] = _permissionService.GetPermissionsByRoleId(Role.RoleId);
                ViewData["Permissions"] = _permissionService.GetAllPermissions();
                return Page();
            }

            int tmpRoleId = _permissionService.UpdateRole(Role);

            isSucces = _permissionService.UpdateRolePermission(tmpRoleId, SelectedPermission);

            if (isSucces)
                return Redirect("/Admin/Roles/ShowRoles");
            else
            {
                ViewData["SelectedPermissions"] = _permissionService.GetPermissionsByRoleId(Role.RoleId);
                ViewData["Permissions"] = _permissionService.GetAllPermissions();
                return Page();
            }
        }
    }
}
