using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn2.Web.Pages.Admin.Roles
{
    [PermissionChecker(9)]
    public class DeleteRoleModel : PageModel
    {
        #region Create Context

        private IPermissionService _permissionService;
        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        #endregion

        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleById(id);
        }

        public IActionResult OnPost()
        {
            bool isSucces;
            if (!ModelState.IsValid)
                return Page();

            isSucces = _permissionService.DeleteRole(Role);

            // TODO : add permission 
            if (isSucces)
                return Redirect("/Admin/Roles/ShowRoles");
            else
                return Page();
        }
    }
}
